# -*- coding: utf-8 -*-
# author: Bhavya Jain
# link: https://github.com/wulfebw/algorithms/blob/master/scripts/dynamic_programming/knapsack.py

def knapSack(W, wt, val, n):
	""" Maximize the value that a knapsack of capacity W can hold. You can either put the item or discard it, there is
	no concept of putting some part of item in the knapsack.

	:param int W: Maximum capacity -in frames- of the knapsack.
	:param list[int] wt: The weights (lengths -in frames-) of each video shot.
	:param list[float] val: The values (importance scores) of each video shot.
	:param int n: The number of the shots.
	:return: A list containing the indices of the selected shots.
	"""
	K = [[0 for _ in range(W + 1)] for _ in range(n + 1)]

	# Build table K[][] in bottom up manner
	for i in range(n + 1):
		for w in range(W + 1):
			if i == 0 or w == 0:
				K[i][w] = 0
			elif wt[i - 1] <= w:
				K[i][w] = max(val[i - 1] + K[i - 1][w - wt[i - 1]], K[i - 1][w])
			else:
				K[i][w] = K[i - 1][w]

	selected = []
	w = W
	for i in range(n, 0, -1):
		if K[i][w] != K[i - 1][w]:
			selected.insert(0, i - 1)
			w -= wt[i - 1]

	return selected


if __name__ == "__main__":
	pass
	""" Driver program to test above function
	val = [0.72, 0.82, 0.33, 0.46, 0.63, 0.86, 0.98, 0.15, 0.62, 0.45, 0.73, 0.81, 0.54, 0.37, 0.47, 0.23, 0.49, 0.51, 0.18, 0.35, 0.37, 0.13]
	wt = [2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2]
	W = 6
	n = len(val)
	selected = knapSack(W, wt, val, n)
	print(selected)
	"""
