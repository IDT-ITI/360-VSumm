# -*- coding: utf-8 -*-
# author: Bhavya Jain
# link: https://github.com/wulfebw/algorithms/blob/master/scripts/dynamic_programming/knapsack.py

import itertools
from collections import OrderedDict


def top_k(val, n):
	""" Maximize the value that a knapsack of capacity W can hold. You can either put the item or discard it, there is
	no concept of putting some part of item in the knapsack.

	:param list[float] val: The values (importance scores) of each video shot.
	:param int n: The number of the video frames.
	:return: A list containing the indices of the selected shots.
	"""

	num_of_selections = round(0.15 * n / 60)

	seq = sorted(val, reverse=True)
	# index = [val.index(s) for s in seq]

	index_2 = []
	for i in range(seq.__len__()):
		ind_2 = [ind for ind, v in enumerate(val) if v == seq[i]]
		index_2.append(ind_2)

	merged = list(itertools.chain.from_iterable(index_2))
	ordered = list(OrderedDict.fromkeys(merged))

	# selected = index[:num_of_selections]
	selected = ordered[:num_of_selections]

	return sorted(selected)


if __name__ == "__main__":
	pass
	""" Driver program to test above function
	val = [0.72, 0.82, 0.33, 0.46, 0.63, 0.86, 0.98, 0.15, 0.62, 0.45, 0.73, 0.81, 0.54, 0.37, 0.47, 0.23, 0.49, 0.51, 0.18, 0.35, 0.37, 0.13]
	n = 1320
	selected = top_k(val, n)
	print(selected)
	"""
