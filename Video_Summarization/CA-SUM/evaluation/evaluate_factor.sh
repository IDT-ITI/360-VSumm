# -*- coding: utf-8 -*-
# The script is taking as granted that the experiments are saved under "base_path" as "base_path/exp$EXP_NUM".
# Run the evaluation script of the `exp_num` experiment for a specific `dataset` and a `regularization factor`.
# Then, compute the fscore (txt file) associated with the above mentioned data-splits and regularization factor.

base_path="/home/apostolid/data/Summaries/360VSumm"
exp_num=$1
dataset=$2
frag_selection=$3  # top_k or knapsack
eval_method=$4  # max or avg
factor=$5 # 0.7

exp_path="$base_path/exp$exp_num/reg$factor"; echo "$exp_path"  # add factor to the path of the experiment

for i in 0 1 2 3 4; do
  results_path="$exp_path/$dataset/results/split$i"
  python evaluation/compute_fscores.py --path "$results_path" --dataset "$dataset" --frag_selection "$frag_selection" --eval "$eval_method"
done
