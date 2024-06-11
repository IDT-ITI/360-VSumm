# -*- coding: utf-8 -*-
# Bash script to automate the procedure of evaluating an experiment.

exp_num=$1
dataset=$2  # 360VSumm
frag_selection=$3  # top_k or knapsack
eval_method=$4 # max or avg

LC_NUMERIC="en_US.UTF-8"
for sigma in $(seq 0.5 0.1 0.9); do
  sh evaluation/evaluate_factor.sh "$exp_num" "$dataset" "$frag_selection" "$eval_method" "$sigma"
done
