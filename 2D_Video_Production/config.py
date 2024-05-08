import argparse

def args():
    parser = argparse.ArgumentParser(description='2d video production arguments')
    parser.add_argument('--saliency_maps_path', type=str,
                        default=r"D:\Program Files\IoanProjects\Vrdata3\saliency\001",
                        help='Directory path of saliency maps')
    parser.add_argument('--frames_path', type=str, default=r"D:\Program Files\IoanProjects\Vrdata3\frames\001",
                        help='Directory path of frames')

    parser.add_argument('--intensity_value', type=int, default=100, help='Intensity value parameter')
    parser.add_argument('--dbscan_distance', type=float, default=1.5, help='DBSCAN distance parameter')
    parser.add_argument('--spatial_distance', type=int, default=85, help='Spatial distance parameter')
    parser.add_argument('--fill_loss', type=int, default=60, help='Fill loss parameter')

    parser.add_argument('--path_to_folder', type=str,
                        default=r'outss',
                        help='Path to save subvideos frames')
    parser.add_argument('--resolution', type=int, nargs=2, default=[960, 1920], help='Resolution parameter')
    # Your code here
    return parser
if __name__ == '__main__':

    args = args()
    print(args.parse_args())