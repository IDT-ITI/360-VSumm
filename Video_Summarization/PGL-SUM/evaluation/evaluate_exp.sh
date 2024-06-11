# -*- coding: utf-8 -*-
# Bash script to automate the procedure of evaluating an experiment.

base_path=".../data/Summaries/360VSumm"
exp_num=$1
dataset=$2  # 360VSumm
frag_selection=$3  # top_k or knapsack
eval_method=$4 # max or avg

exp_path="$base_path/exp$exp_num"; echo $exp_path

for i in 0 1 2 3 4; do
  results_path="$exp_path/$dataset/results/split$i"
  python evaluation/compute_fscores.py --path $results_path --dataset $dataset --frag_selection $frag_selection --eval $eval_method
done
