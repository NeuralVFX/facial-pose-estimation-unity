![](examples/example_celeb.gif)
# Facial-Pose-Estimation-Unity

This repository is a Unity Project demonstrating realtime AR Facial Pose Estimation. This utilizes a plugin DLL from my [facial-pose-estimation-opencv](https://github.com/NeuralVFX/facial-pose-estimation-opencv) project and a custom trained Neural Net from my [facial-pose-estimation-pytorch](https://github.com/NeuralVFX/facial-pose-estimation-pytorch) project.

## About
This is one of three repositories which together form a larger project, these are the three repositories:
- [facial-pose-estimation-pytorch](https://github.com/NeuralVFX/facial-pose-estimation-pytorch)
- [facial-pose-estimation-opencv](https://github.com/NeuralVFX/facial-pose-estimation-opencv) 
- [facial-pose-estimation-unity](https://github.com/NeuralVFX/facial-pose-estimation-unity) - You are here.

This blog post describes the whole project: [AR Facial Pose Estimation](http://neuralvfx.com/augmented-reality/ar-facial-pose-estimation/)


## Extra Info
- This runs live on a desktop
- Tracks both the transform of the entire head, and the pose of the face
- Tracks a single face at a time, if two people stand in front of the camera the face will jump back and forth
- There are controls built in for manually adjusting camera FOV
- The `Neural Net` used by the project can be found here: [facial-pose-estimation-pytorch](https://github.com/NeuralVFX/facial-pose-estimation-pytorch)

## Estimation Pipeline Diagram
![](examples/unity_pipeline_b.png)

## Code Usage
Usage instructions found here: [user manual page](USAGE.md).




