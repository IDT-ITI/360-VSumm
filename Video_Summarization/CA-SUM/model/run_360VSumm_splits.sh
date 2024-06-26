# -*- coding: utf-8 -*-
# Bash script to automate the training of the model for the `SumMe` dataset.
# Runs main.py, for each data-split and valid values of `reg_factor`, for the selected number of epochs and batch size.

LC_NUMERIC="en_US.UTF-8"
for i in $(seq 0 4); do
  for sigma in $(seq 0.5 0.1 0.9); do
    python model/main.py --split_index "$i" --n_epochs 600 --batch_size 32 --video_type '360VSumm' --reg_factor "$sigma"
  done
done