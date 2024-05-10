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


