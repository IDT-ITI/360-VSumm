# 360-VSumm
* from **A Human-Annotated Video Dataset for Training and Evaluation of 360-Degree Video Summarization Methods**
* Written by Ioannis Kontostathis, Evlampios Apostolidis, Vasileios Mezaris
* This dataset can be used for training/evaluating deep-learning models for 360-degree video summarization.
## Dataset
The provided dataset contains 40 videos featuring diverse visual content, including sports games, short movies, documentaries and underwater activites, ranging from 1 to 4 minutes. The dataset was formulated based on the [VR-EyeTracking dataset](https://github.com/mtliba/ATSal/tree/master), where videos have been captured using both static and moving cameras. Ground-truth annotations are included for the fragments selected for summary, along with saliency scores for each frame. The dataset is available here

## 2D Video production
The 40 videos of the dataset was created using the 2D Video production algorithm of [Kontostathis et al](https://github.com/IDT-ITI/CA-SUM-360). We used the videos and ground-thruth saliency maps of the re-produced version of the VR-EyeTraking dataset, that is publicly-available [here](https://mtliba.github.io/Reproduced-VR-EyeTracking/). After applying the algorithm to the entire dataset, we ended up with 40 videos that meet the following criteria: dynamic and diverse visual content, with a duration longer than 1 minute. In contrast with Kontostathis et al, for the 2D Video production algorithm we set the parameters intensity *t1*, dbscan distance *t2*, spatial distance *t3* and missing frame *t4* to *100*, *1.5*, *85* and *60*, respectively. 

## Annotator Software
To assist annotators in their task, we implemented a graphical user interface tool which facilitates easy navigation throughout the video. Users can choose fragments for inclusion in the summary with simple clicks, while there is also an option to check the selected summary video separately, before choosing the final fragments. 

The software was implemented in C# and the source code is available in the [Annotation_Tool](Annotation_Tool) directory. Visual Studio 2022 is needed to compile the source code.

# Licence
This code is provided for academic, non-commercial use only. Please also check for any restrictions applied in the code parts and datasets used here from other sources. For the materials not covered by any such restrictions, redistribution and use in source and binary forms, with or without modification, are permitted for academic non-commercial use provided that the following conditions are met:

Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer. Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation provided with the distribution.

This software is provided by the authors "as is" and any express or implied warranties, including, but not limited to, the implied warranties of merchantability and fitness for a particular purpose are disclaimed. In no event shall the authors be liable for any direct, indirect, incidental, special, exemplary, or consequential damages (including, but not limited to, procurement of substitute goods or services; loss of use, data, or profits; or business interruption) however caused and on any theory of liability, whether in contract, strict liability, or tort (including negligence or otherwise) arising in any way out of the use of this software, even if advised of the possibility of such damage.

# Citation
If you find our pruning method or pruned models useful in your work, please cite the following publication where this approach was proposed:

# Acknowledgements
This work was supported by the EU Horizon Europe and Horizon 2020 programmes under grant agreements 101070109 TransMIXR and 951911 AI4Media, respectively


