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

The code for training and evaluating the utilized video summarization model (saliency-aware variant of [CA-SUM](https://github.com/e-apostolidis/CA-SUM)), was checked and verified on an `Ubuntu 20.04.3` PC with an `NVIDIA RTX 2080Ti` GPU and an `i5-11500K` CPU. Main packages required:
<div align="center">
  <table>
    <tr>
      <th>Python</th>
      <th>PyTorch</th>
      <th>CUDA Version</th>
      <th>cuDNN Version</th>
      <th>TensorBoard</th>
      <th>TensorFlow</th>
      <th>Numpy</th>
      <th>H5py</th>
    </tr>
    <tr>
      <td>3.8(.8)</td>
      <td>1.7.1</td>
      <td>11.0</td>
      <td>8005</td>
      <td>2.4.0</td>
      <td>2.4.1</td>
      <td>1.20.2</td>
      <td>2.10.0</td>
    </tr>
  </table>
</div>

## Dataset
The created dataset contains 40 2D-videos with diverse visual content (including sports games, short movies, documentaries and underwater activites) and a duration that ranges between 1 and 4 minutes. These videos were created by applying the 2D video production algorithm from [Kontostathis et al](https://github.com/IDT-ITI/CA-SUM-360) to 40 360-degrees videos from the [VR-EyeTracking dataset](https://github.com/mtliba/ATSal/tree/master), using the ground-truth saliency maps for the videos of the VR-EyeTraking dataset, that are publicly-available [here](https://mtliba.github.io/Reproduced-VR-EyeTracking/). Please note that for 2D video production, we set the parameters *t1* (intensity), *t2* (dbscan distance), *t3* (spatial distance) and *t4* (missing frame) of the algorithm, equal to *100*, *1.5*, *85* and *60*, respectively.

Add info about ground-truth data (Lampis)

## Video summarization
CA-SUM, PGL-SUM

## Annotation Tool
To assist annotators in their task, we implemented a graphical user interface tool which facilitates easy navigation throughout the video. Users can choose fragments for inclusion in the summary with simple clicks, while there is also an option to check the selected summary video separately, before choosing the final fragments. 

<p align="center">
  <img src="annotationTool.png" alt="The graphical interface of the annotation tool" />
  <br><em>The graphical interface of the annotation tool.</em>
</p>
The software was implemented in C# and the source code is available in the [Annotation_Tool](Annotation_Tool) directory. Visual Studio 2022 is needed to compile the source code.

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
This work was supported by the EU's Horizon Europe and Horizon 2020 programmes under grant agreements 101070109 TransMIXR and 951911 AI4Media, respectively

