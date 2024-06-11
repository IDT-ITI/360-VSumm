# A Human-Annotated Video Dataset for Training and Evaluation of 360-Degree Video Summarization Methods
* From **A Human-Annotated Video Dataset for Training and Evaluation of 360-Degree Video Summarization Methods** Proc. 1st Int. Workshop on Video for Immersive Experiences (Video4IMX-2024) at ACM IMX 2024, Stockholm, Sweden, June 2024.
* Written by Ioannis Kontostathis, Evlampios Apostolidis, Vasileios Mezaris
* This dataset can be used for training and evaluating deep-learning models for 360-degree video summarization.

## Main dependencies
The code for the 2D Video production algorithm, was checked and verified on a `Windows 11` PC with an `NVIDIA GeForce GTX 1080Ti` GPU and an `i5-12600K` CPU. Main packages required:
<div align="center">
  <table>
    <tr>
      <th>Python</th>
      <th>Numpy</th>
      <th>Opencv</th>
      <th>Scipy</th>
      <th>Scikit-learn</th>
    </tr>
    <tr>
      <td>3.8</td>
      <td>1.24.3</td>
      <td>4.6.0</td>
      <td>1.8.1</td>
      <td>1.2.2</td>
    </tr>
  </table>
</div>

The code for training and evaluating the utilized video summarization models ([PGL-SUM](https://github.com/e-apostolidis/PGL-SUM), [CA-SUM](https://github.com/e-apostolidis/CA-SUM) and their saliency-aware variants), was checked and verified on an `Ubuntu 20.04.6 LTS` PC with an `NVIDIA RTX 3090` GPU and an `i5-11600K` CPU. Main packages required:
<div align="center">
  <table>
    <tr>
      <th>Python</th>
      <th>Torch</th>
      <th>CUDA Version</th>
      <th>TensorBoard</th>
      <th>TensorFlow</th>
      <th>Numpy</th>
      <th>H5py</th>
    </tr>
    <tr>
      <td>3.8(.18)</td>
      <td>2.1.2</td>
      <td>11.8</td>
      <td>2.12.1</td>
      <td>2.3.0</td>
      <td>1.24.4</td>
      <td>2.10.0</td>
    </tr>
  </table>
</div>

## Dataset
The created dataset contains 40 2D-videos with diverse visual content (including sports games, short movies, documentaries and underwater activites) and a duration that ranges between 1 and 4 minutes. These videos were created by applying the 2D video production algorithm from [Kontostathis et al](https://github.com/IDT-ITI/CA-SUM-360) to [40 360-degrees videos](https://github.com/IDT-ITI/360-VSumm/blob/main/2D_Video_Production/Selected_videos_VR-EyeTracking.txt) from the [VR-EyeTracking dataset](https://github.com/mtliba/ATSal/tree/master), using the ground-truth saliency maps for the videos of the VR-EyeTraking dataset, that are publicly-available [here](https://mtliba.github.io/Reproduced-VR-EyeTracking/). Please note that for 2D video production, we set the parameters *t1* (intensity), *t2* (dbscan distance), *t3* (spatial distance) and *t4* (missing frame) of the algorithm, equal to *100*, *1.5*, *85* and *60*, respectively.

To reproduce the 40 videos, download the VR-EyeTracking dataset. Then, to extract the ERP frames for each video use the [frames_extractor.py](https://github.com/IDT-ITI/360-VSumm/blob/main/2D_Video_Production/frames_extractor.py) script and run the following command:
```
python frames_extractor.py --path_to_videos "PATH/path_containing_the_VR-EyeTracking_360_videos" --output_folder "PATH/path_to_save_erp_frames_for_each_video"
```
Given the extracted saliency maps and the frames for the videos of the VR-EyeTracking, to produced the conventional 40 2D videos, use the [main.py](https://github.com/IDT-ITI/360-VSumm/blob/main/2D_Video_Production/main.py) script and run the following command:
```
python main.py --video_frames_path "PATH/path_to_VrEyeTracking\frames" --saliency_maps_path "PATH/path_to_VrEyeTracking\saliency" --intensity_value 100 --dbscan_distance 1.5 --spatial_distance 85 --fill_loss 60
```
To train and evaluate the video summarization models, we used the created [360VSumm.h5](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/data/360VSumm.h5) file, which has the following structure:
```Text
/key
    /change_points            2D-array with shape (num_segments, 2), where each row stores the indices of the starting and ending frame of a video segment
    /features                 2D-array with shape (n_steps, 1024), where each row stores the feature vector of the relevant video frame (GoogleNet features)
    /gtscore                  1D-array with shape (n_frames, 1), where each row contains a value indicating the importance of the corresponding video frame according to the users' annotations (after averaging them)
    /n_frames                 number of video frames
    /n_steps                  number of sampled frames
    /picks                    1D-array with shape (n_steps, 1) with the indices of the sampled frames
    /saliency scores          1D-array with shape (n_steps, 1) with the computed saliency scores for the sampled frames
    /user_summary             2D-array with shape (15, n_frames), where each row is a binary vector indicating the frames of the video that selected (value=1) or not (value=0) for inclusion in the summary by each human annotator
```
</div>

## Annotation Tool
To facilitate the annotation process, we developed a tool with a user-friendly graphical interface that is depicted below. To start the annotation process, the participants have to load a video from the collection by clicking on the corresponding button on the upper right part of the interface. Subsequently, they see the first frame of the selected video in the video player of the tool and the sub-fragments of the video in the white-coloured area between the video player and the navigation buttons. Each dash in this area corresponds to a different sub-fragment and it is clickable to allow the selection of the relevant part of the video. Moreover, through the user interface the annotators are notified about the number of sub-fragments that should be selected in order to form a summary according to the targeted time budget. When the necessary number of sub-fragments is reached, the annotators can immediately check the formed summary by clicking on the relevant button. This action opens a new smaller video player (which appears in the left side of the main video player that plays only the chosen parts of the video. If the annotator needs to make changes in the summary, s/he can replace one or more of the selected sub-fragments, by clicking once more on the relevant dashes (to un-select them) and click on other dashes (to select them). Through this process, the annotators can update the summary as many times as they wish, and can re-check it by clicking on the “Check Summary” button. After concluding to an optimal video summary, they have to click on the “Save annotations...” button in order to store their preferences for the annotated video. This process produces a txt file per video, containing information about the starting and ending frame of each selected sub-fragment of the video. As a note, the review of the formed summary is possible only when the correct number of sub-fragments has been selected. If the selected sub-fragments are less/more that the suggested ones, the relevant text in the user interface is coloured gray/red, while after clicking on the “Check Summary” button, the annotator is notified that “the number of selected fragments is lower/higher than the suggested one”. Moreover, the interface is equipped with the standard functionalities for navigation in the video, i.e., buttons for play/stop, frame-by-frame display (“>” and “<”), and 10 frames skip (“«” and “»”), while the annotators can navigate also via the horizontal scroll bar at the bottom of the interface.

<p align="center">
  <img src="annotationTool.png" alt="The graphical interface of the annotation tool" />
  <br><em>The graphical interface of the annotation tool.</em>
</p>

The software was implemented in C# and the source code is available in the [Annotation_Tool](https://github.com/IDT-ITI/360-VSumm/tree/main/Annotation_Tool) directory. Please use Visual Studio 2022 to compile the source code.

## Video summarization
### PGL-SUM
To train the PGL-SUM method using the created splits of the VSumm dataset (available [here](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/data/360VSumm_splits.json)) use the code within the [PGL-SUM](https://github.com/IDT-ITI/360-VSumm/tree/main/Video_Summarization/PGL-SUM) directory. Then,
 - in [`configs.py`](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/PGL-SUM/model/configs.py), define the directory where the analysis results will be saved to. </div>
 - in [`data_loader.py`](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/PGL-SUM/model/data_loader.py), specify the path to the h5 file of the used dataset, and the path to the JSON file containing data about the utilized data splits.
Finally, run the following commmand:
```shell-script
sh /model/run_360VSumm_splits.sh
```
To train the saliency-aware variant of the PGL-SUM method use the [solver_sal.py](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/PGL-SUM/model/solver_sal.py) instead of the originally used [solver.py](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/PGL-SUM/model/solver.py) (e.g., temporarily rename the "solver.py" file as "solver_no_sal.py" and the "solver_sal.py" file as "solver.py") and run the aforementioned command.

Please note that after each training epoch the algorithm performs an evaluation step, using the trained model to compute the importance scores for the frames of each video of the test set. These scores are then used by the provided [evaluation](https://github.com/IDT-ITI/360-VSumm/tree/main/Video_Summarization/PGL-SUM/evaluation) scripts to assess the overall performance of the model (in F-Score). To perform the evaluation, specify the path to the h5 file of the used dataset in the [compute_fscores.py](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/PGL-SUM/evaluation/compute_fscores.py#L27) file, and run the following command:
```shell-script
sh /evaluation/evaluate_exp.sh i 360VSumm top_k max
```
where, _i_ must be the one from the directory where the analysis results are stored; i.e., it should be 1 if the results are stored in the ".../Summaries/360VSumm/exp1" directory

To re-produce the reported results in Table 3 of the paper, run the above described process after setting the number of local attention mechanisms and the number of heads in the global and local attention mechanism. To set up the former two parameters, use the [configs.py](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/PGL-SUM/model/configs.py) script and update the "--n_segments" and "--heads", respectively. To set up the last one, update the number of heads at the [summarizer.py](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/PGL-SUM/model/layers/summarizer.py#L34) script.

The progress of the training can be monitored via the TensorBoard platform and by:
- opening a command line (cmd) and running: `tensorboard --logdir=/path/to/log-directory --host=localhost`
- opening a browser and pasting the returned URL from cmd. </div>

### CA-SUM
To train the CA-SUM method using the created splits of the VSumm dataset (available [here](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/data/360VSumm_splits.json)) use the code within the [CA-SUM](https://github.com/IDT-ITI/360-VSumm/tree/main/Video_Summarization/CA-SUM) directory. Then,
 - in [`configs.py`](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/CA-SUM/model/configs.py), define the directory where the analysis results will be saved to. </div>
 - in [`data_loader.py`](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/CA-SUM/model/data_loader.py), specify the path to the h5 file of the used dataset, and the path to the JSON file containing data about the utilized data splits.
Finally, run the following commmand:
```shell-script
sh /model/run_360VSumm_splits.sh 
```
To train the saliency-aware variant of the CA-SUM method use the [solver_sal.py](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/CA-SUM/model/solver_sal.py) instead of the originally used [solver.py](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/CA-SUM/model/solver.py) (e.g., temporarily rename the "solver.py" file as "solver_no_sal.py" and the "solver_sal.py" file as "solver.py") and run the aforementioned command.

Please note that after each training epoch the algorithm performs an evaluation step, using the trained model to compute the importance scores for the frames of each video of the test set. These scores are then used by the provided [evaluation](https://github.com/IDT-ITI/360-VSumm/tree/main/Video_Summarization/CA-SUM/evaluation) scripts to assess the overall performance of the model (in F-Score). To perform the evaluation, specify the path to the h5 file of the used dataset in the [compute_fscores.py](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/CA-SUM/evaluation/compute_fscores.py#L27) file, and run the following command:
```shell-script
sh /evaluation/evaluate_exp.sh i 360VSumm top_k max
```
where, _i_ must be the one from the directory where the analysis results are stored; i.e., it should be 2 if the results are stored in the ".../Summaries/360VSumm/exp2" directory

To re-produce the reported results in the upper part of Table 4 of the paper, run the above described process after setting the block size equal to 60. To do this, use the [configs.py](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/CA-SUM/model/configs.py#L66) script and update "--block_size", accordingly. To re-produce the reported results in the lower part of Table 4 of the paper, run the above described process after fixing the sigma parameter in the [run_360VSumm_splits.sh](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/CA-SUM/model/run_360VSumm_splits.sh#L7C3-L7C38) shell srcipt equal to 0.7, and setting the block size as needed in [configs.py](https://github.com/IDT-ITI/360-VSumm/blob/main/Video_Summarization/CA-SUM/model/configs.py#L66).

As before, the progress of the training can be monitored via the TensorBoard platform and by:
- opening a command line (cmd) and running: `tensorboard --logdir=/path/to/log-directory --host=localhost`
- opening a browser and pasting the returned URL from cmd. </div>

# Licence
This code is provided for academic, non-commercial use only. Please also check for any restrictions applied in the code parts and datasets used here from other sources. For the materials not covered by any such restrictions, redistribution and use in source and binary forms, with or without modification, are permitted for academic non-commercial use provided that the following conditions are met:

Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation provided with the distribution.

This software is provided by the authors "as is" and any express or implied warranties, including, but not limited to, the implied warranties of merchantability and fitness for a particular purpose are disclaimed. In no event shall the authors be liable for any direct, indirect, incidental, special, exemplary, or consequential damages (including, but not limited to, procurement of substitute goods or services; loss of use, data, or profits; or business interruption) however caused and on any theory of liability, whether in contract, strict liability, or tort (including negligence or otherwise) arising in any way out of the use of this software, even if advised of the possibility of such damage.

# Citation
<div align="justify">
If you find our method useful in your work or you use some materials provided in this repo, please cite the following publication: 

I. Kontostathis, E. Apostolidis, V. Mezaris, "<b>A Human-Annotated Video Dataset for Training and Evaluation of 360-Degree Video Summarization Methods</b>", Proc. 1st Int. Workshop on Video for Immersive Experiences (Video4IMX-2024) at ACM IMX 2024, Stockholm, Sweden, June 2024.
</div>

# Acknowledgements
This work was supported by the EU's Horizon Europe and Horizon 2020 programmes under grant agreements 101070109 TransMIXR and 951911 AI4Media, respectively.

