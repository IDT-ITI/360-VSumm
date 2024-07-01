
import cv2
import numpy as np
import os
import re
import argparse

def videoCreator(output_path,video,final_resolution):
    folder_path = output_path
    fps=30
    def extract_numerical_part(folder_name):
        if isinstance(folder_name, str):
            match = re.search(r'\d+', folder_name)
            if match:
                return int(match.group())
        return float('inf')

    width = int(final_resolution[1] * 75 / 360)
    height = int(width * 3 / 4)

    output_path = output_path+f"/2Dvideo_{video}.mp4"

    frames = os.listdir(folder_path)
    frames = sorted(frames, key=extract_numerical_part)
    fourcc = cv2.VideoWriter_fourcc(*'mp4v')
    video_writer = cv2.VideoWriter(output_path, fourcc, fps, (width, height))
    for frame_file in frames:

        current_frame = cv2.imread(os.path.join(folder_path, frame_file))
        video_writer.write(current_frame)

    cv2.destroyAllWindows()
    files = os.listdir(folder_path)
    for file in files:
        # Check if the file is a .png file
        if file.endswith(".png"):
            file_path = os.path.join(folder_path, file)
            # Remove the file
            os.remove(file_path)
    print("Output video",output_path)

def xyz2lonlat(xyz):
    atan2 = np.arctan2
    asin = np.arcsin

    norm = np.linalg.norm(xyz, axis=-1, keepdims=True)
    xyz_norm = xyz / norm
    x = xyz_norm[..., 0:1]
    y = xyz_norm[..., 1:2]
    z = xyz_norm[..., 2:]

    lon = atan2(x, z)
    lat = asin(y)
    lst = [lon, lat]

    out = np.concatenate(lst, axis=-1)
    return out


def lonlat2XY(lonlat, shape):
    X = (lonlat[..., 0:1] / (2 * np.pi) + 0.5) * (shape[1] - 1)
    Y = (lonlat[..., 1:] / (np.pi) + 0.5) * (shape[0] - 1)
    lst = [X, Y]
    out = np.concatenate(lst, axis=-1)

    return out


class Equirectangular:
    def __init__(self, img_name,final_resolution):
        self._img = cv2.imread(img_name, cv2.IMREAD_COLOR)

        self._img = cv2.resize(self._img,(final_resolution[0],final_resolution[1]))

        [self._height, self._width,_] =  self._img.shape


    def GetPerspective(self, FOV, THETA, PHI, height, width):
        #
        # THETA is left/right angle, PHI is up/down angle, both in degree
        #
        f = 0.5 * width * 1 / np.tan(0.5 * FOV / 180.0 * np.pi)
        cx = (width - 1) / 2.0
        cy = (height - 1) / 2.0
        K = np.array([
            [f, 0, cx],
            [0, f, cy],
            [0, 0, 1],
        ], np.float32)
        K_inv = np.linalg.inv(K)

        x = np.arange(width)
        y = np.arange(height)
        x, y = np.meshgrid(x, y)
        z = np.ones_like(x)
        xyz = np.concatenate([x[..., None], y[..., None], z[..., None]], axis=-1)
        xyz = xyz @ K_inv.T

        y_axis = np.array([0.0, 1.0, 0.0], np.float32)
        x_axis = np.array([1.0, 0.0, 0.0], np.float32)
        R1, _ = cv2.Rodrigues(y_axis * np.radians(THETA))
        R2, _ = cv2.Rodrigues(np.dot(R1, x_axis) * np.radians(PHI))
        R = R2 @ R1
        xyz = xyz @ R.T

        lonlat = xyz2lonlat(xyz)
        XY = lonlat2XY(lonlat, shape=self._img.shape).astype(np.float32)
        persp = cv2.remap(self._img, XY[..., 0], XY[..., 1], cv2.INTER_CUBIC, borderMode=cv2.BORDER_WRAP)
        return persp


def fov_extractor(centers,img_path,path_to_videos_frames,final_resolution,output_path):
    counters=0
    for k, items in enumerate(centers):
        width = int(final_resolution[1] * 75 / 360)
        height = int(width * 3 / 4)
        equ = Equirectangular(os.path.join(path_to_videos_frames,img_path[k]),final_resolution)  # Load equirectangular image
        x = items[0]
        y = items[1]
        y = final_resolution[0] - y
        longitude = (x / final_resolution[1]) * 360 - 180
        latitude = (y / final_resolution[0]) * 180 - 90

        img = equ.GetPerspective(85, longitude, latitude, height,width)  # Specify parameters(FOV, theta, phi, height, width)
        if not os.path.exists(output_path):
            # If it doesn't exist, create it
            os.makedirs(output_path)
        cv2.imwrite(output_path + '/' + f'{counters:06d}.png', img)
        counters += 1

if __name__ == '__main__':
    parser = argparse.ArgumentParser(description='Create 360-VSumm videos')
    parser.add_argument('path_to_video_frames', type=str, help="Path to the directory containing the video's folders, each folder containing the frames")
    args = parser.parse_args()
    path_to_videos= args.path_to_video_frames
    final_resolution = [960, 720]
    output_path = "360-VSumm"
    with open('selected_videos_VR_EyeTracking.txt', 'r') as file:
        # Read all lines from the file
        data = file.read()
        # Split the content into a list of numbers
        numbers_list = data.strip().split(',')
        # Convert the strings to integers
        numbers_list = [num for num in numbers_list]

    for j,vd_txt in enumerate(numbers_list):
        coordinates = []
        frame_numbers = []
        with open(os.path.join("selected_centers",vd_txt+".txt"), 'r') as file:
            for line in file:
                # Split the line into coordinate part and frame number part
                coord_part, frame_part = line.split(') ')

                # Remove the opening parenthesis and split the coordinates by comma
                coord = coord_part.strip('(').split(',')
                x = int(coord[0])
                y = int(coord[1])
                coordinates.append([x, y])

                # Convert the frame part to an integer
                frame = f"{int(frame_part):04d}.png"
                frame_numbers.append(frame)

        frames_folder = os.path.join(path_to_videos,vd_txt)
        fov_extractor(coordinates, frame_numbers, frames_folder,final_resolution, output_path)
        videoCreator(output_path, j, final_resolution)
