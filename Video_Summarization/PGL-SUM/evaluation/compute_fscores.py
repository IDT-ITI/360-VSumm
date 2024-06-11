# -*- coding: utf-8 -*-
from os import listdir
import json
import numpy as np
import h5py
from evaluation_metrics import evaluate_summary
from generate_summary import generate_summary
import argparse

# arguments to run the script
parser = argparse.ArgumentParser()
parser.add_argument("--path", type=str,
                    default='.../Summaries/360VSumm/exp1/360VSumm/results/split0',
                    help="Path to the json files with the scores of the frames for each epoch")
parser.add_argument("--dataset", type=str, default='360VSumm', help="Dataset to be used")
parser.add_argument("--frag_selection", type=str, default="top_k", help="Fragment selection method for building the summary (top_k or knapsack)")
parser.add_argument("--eval", type=str, default="max", help="Eval method to be used for f_score reduction (max or avg)")

args = vars(parser.parse_args())
path = args["path"]
dataset = args["dataset"]
frag_selection = args["frag_selection"]
eval_method = args["eval"]

results = [f for f in listdir(path) if f.endswith(".json")]
results.sort(key=lambda video: int(video[9:-5]))
dataset_path = '...data/360VSumm.h5'

f_score_epochs = []
for epoch in results:                       # for each epoch ...
    all_scores = []
    with open(path + '/' + epoch) as f:     # read the json file ...
        data = json.loads(f.read())
        keys = list(data.keys())

        for video_name in keys:                    # for each video inside that json file ...
            scores = np.asarray(data[video_name])  # read the importance scores from frames
            all_scores.append(scores)

    all_user_summary, all_shot_bound, all_nframes, all_positions = [], [], [], []
    with h5py.File(dataset_path, 'r') as hdf:
        for video_name in keys:
            user_summary = np.array(hdf.get(video_name + '/user_summary'))
            sb = np.array(hdf.get(video_name + '/change_points'))
            n_frames = np.array(hdf.get(video_name + '/n_frames'))
            positions = np.array(hdf.get(video_name + '/picks'))

            all_user_summary.append(user_summary)
            all_shot_bound.append(sb)
            all_nframes.append(n_frames)
            all_positions.append(positions)

    all_summaries = generate_summary(all_shot_bound, all_scores, all_nframes, all_positions, frag_selection)

    all_f_scores = []
    # compare the resulting summary with the ground truth one, for each video
    for video_index in range(len(all_summaries)):
        summary = all_summaries[video_index]
        user_summary = all_user_summary[video_index]
        f_score = evaluate_summary(summary, user_summary, eval_method)
        all_f_scores.append(f_score)

    f_score_epochs.append(np.mean(all_f_scores))
    num_epoch = epoch.split(".")[0][9:]
    # print(f"[epoch {num_epoch}] f_score: {np.mean(all_f_scores)}")

# Save the importance scores in txt format.
with open(path + '/f_scores_' + frag_selection + '_' + eval_method + '.txt', 'w') as outfile:
    for f_score in f_score_epochs:
        outfile.write('%s\n' % f_score)
