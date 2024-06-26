
from sklearn.cluster import DBSCAN
from scipy.spatial.distance import cdist
import copy
import cv2
import numpy as np

def pixel_to_latlon(x, y, image_shape):
    height, width = image_shape

    lon = (x / width) * 360.0 - 180.0
    lat = (y / height) *180.0 - 90.0
    return lat, lon

def haversine(coord1, coord2):
    lat1, lon1 = np.radians(coord1)
    lat2, lon2 = np.radians(coord2)
    dlat = lat2 - lat1
    dlon = lon2 - lon1
    a = np.sin(dlat / 2) ** 2 + np.cos(lat1) * np.cos(lat2) * np.sin(dlon / 2) ** 2
    c = 2 * np.arctan2(np.sqrt(a), np.sqrt(1 - a))
    return c

def dbscan_clustering(saliency_features, saliency_points, epsilon, min_samples, image_shape):
    saliency_points_latlon = [pixel_to_latlon(x, y, image_shape) for x, y in saliency_points]
    distances = cdist(saliency_points_latlon, saliency_points_latlon, metric=haversine)
    #print(saliency_points)

    distances[np.isnan(distances)] = 10
    # Initialize the DBSCAN algorithm
    dbscan = DBSCAN(eps=epsilon, min_samples=1)

    cluster_assignments = dbscan.fit_predict(distances)

    unique_labels = np.unique(cluster_assignments)

    clustered_points_1 = [[] for _ in range(len(unique_labels))]  # Adjust size of clustered_points
    outliers = []
    count = 0
    #print(cluster_assignments)
    #print(saliency_features)
    for point, label in zip(saliency_features, cluster_assignments):
        if label != -1:
            clustered_points_1[label].append(point)
        else:
            outliers.append(point)

    flag = False

    clusters = copy.deepcopy(clustered_points_1)

    for i, lst in enumerate(clustered_points_1):
        for item in lst:
            if count == 0:
                count += 1
                point_new = item[0]

            else:
                if abs(point_new - item[0]) > 600:

                    flag = True
                    hold_count = i
        count = 0
        if flag == True:
            for j, item in enumerate(clustered_points_1[hold_count]):
                if (image_shape[1]- item[0]) >300:
                    clustered_points_1[hold_count][j] = item[0] + image_shape[1], item[1], item[2], item[3]
            flag == False

    #print(cluster_assignments)
    #print(clustered_points)
    return clusters, cluster_assignments,clustered_points_1
c=0
def extract_salient_regions(saliency_map,intensity_value,dbscan_distance,resolution):

    saliency_map = saliency_map

    saliency_map_filtered = saliency_map.copy()
    saliency_map_filtered[saliency_map <= intensity_value] = 0

    # Step 2: Find contours and bounding boxes
    contours, _ = cv2.findContours(saliency_map_filtered, cv2.RETR_EXTERNAL, cv2.CHAIN_APPROX_SIMPLE)

    # Step 3: Extract bounding boxes
    bounding_box_era = []
    bounding_boxes = []
    #print(contours)
    for contour in contours:

        x, y, w, h = cv2.boundingRect(contour)

        bounding_boxes.append((x, y, w, h))


    # Step 4: Feature extraction (using bounding boxes as features)
    saliency_features = bounding_boxes
    saliency_features = sorted(saliency_features)
    features = [(sublist[0],sublist[1]) for sublist in saliency_features]

    #print(saliency_features)
    clusters,labels,clusters_1 = dbscan_clustering(saliency_features,features,dbscan_distance,1,(resolution[0],resolution[1]))


    final = []

    for i, cluster in enumerate(clusters_1):
        final.append(np.mean(cluster, axis=0, dtype=int))


    for i,item in enumerate(final):
        if item[0]>resolution[1]:
            final[i][0] = item[0]-resolution[1]


    final = [tuple(array) for array in final]

    return final