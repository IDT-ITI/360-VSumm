import cv2
import os
import argparse
import ffmpeg

current_script_path = os.path.abspath(__file__)
parent_directory = os.path.dirname(current_script_path)
grant_parent_directory = os.path.dirname(parent_directory)


def ERP_videos_to_frames(path_to_videos,video_names, output_folder_path):
    if not os.path.exists(output_folder_path):
        os.makdirs(output_folder_path)
    for video in video_names:
        # Read the video from specified path if your videos are in other format change extention .mp4
        video_ = cv2.VideoCapture(os.path.join(path_to_videos, video+".mp4"))
        print(os.path.join(path_to_videos, video+".mp4"))
        folder_path = output_folder_path + "/" + video
        if not os.path.exists(folder_path):
            os.makedirs(folder_path)

        count = 0
        while (True):
            # reading from frame
            ret, frame = video_.read()
            if ret:

                name = os.path.join(folder_path, f"{count:04d}.png")
                # writing the extracted images
                cv2.imwrite(name, frame)

                count += 1
            else:
                break
        # Release all space and windows once done
        video_.release()
        cv2.destroyAllWindows()


if __name__ == "__main__":
    parser = argparse.ArgumentParser(description='Extract frames from a video.')

    parser.add_argument('--path_to_videos', type=str, default=r'..vreyetracking',
                        help='Path to VR-EyeTracking videos')
    parser.add_argument('--output_folder', type=str, default=r'erp_frames',
                        help='Folder to save extracted frames')

    args = parser.parse_args()

    path_to_videos = args.path_to_videos

    output_folder = args.output_folder

    video_names_txt = "Selected_videos_VR-EyeTracking.txt"
    with open(video_names_txt, 'r') as file:
        content = file.read().strip()  # Read the file content and strip any extra whitespace

    # Split the content by commas to get individual values
    video_names = content.split(',')

    # Convert values to integers (optional, if needed)
    video_names = [value for value in video_names]
    print(video_names[0])
    print(output_folder)
    if os.path.exists(output_folder):
        print(output_folder + " exists")
    else:
        os.mkdir(output_folder)

    ERP_videos_to_frames(path_to_videos,video_names, output_folder)





