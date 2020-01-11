
## Getting Started
- Install Unity (using 2018.2.8f1)
- Download and compile `OpenCV 4.1.1` from https://opencv.org/
- Download and compile `Dlib 19.17` from http://dlib.net/

- Follow the directions at [facial-pose-estimation-opencv](https://github.com/NeuralVFX/facial-pose-estimation-opencv/) to compile `facial-pose-estimation-opencv.dll` (Using the included might not work on all systems)

- Follow the directions in [facial-pose-estimation-pytorch](https://github.com/NeuralVFX/facial-pose-estimation-pytorch/) to train the facial pose detection network, or simply download this [ONNX Model](https://github.com/NeuralVFX/facial-pose-estimation-pytorch/blob/master/output/test_run_3_opt.onnx)

- Clone this repo:

```bash
git clone https://github.com/NeuralVFX/facial-pose-estimation-unity.git
```

## Setting Up Plugin

#### Plugin DLLs
- Copy `.dll` files from `OpenCV` `Bin` folder into `facial-pose-estimation-unity\Assets\Plugins`
- Copy `facial-pose-estimation-opencv.dll` into  `facial-pose-estimation-unity\Assets\Plugins`

#### Models
- Download the `SSD`, `Landmark Detection`, and `Facial Pose Estimation` models and place into `facial-pose-estimation-opencv`
- Rename the `Facial Pose Estimation` model `opt_model.onnx`

| **Model**                    | **Link**                                  |
|------------------------------|--------------------------------------------|
| `Facial Pose Estimation Model`|[ONNX Model](https://github.com/NeuralVFX/facial-pose-estimation-pytorch/blob/master/output/test_run_3_opt.onnx)|
| `Face Detection SSD Meta`                   | [deploy.prototxt](https://github.com/spmallick/learnopencv/blob/master/FaceDetectionComparison/models/deploy.prototxt) |
| `Face Detection SSD Model`                  |    [res10_300x300_ssd_iter_140000_fp16.caffemodel](https://github.com/spmallick/learnopencv/raw/master/FaceDetectionComparison/models/res10_300x300_ssd_iter_140000_fp16.caffemodel)                                        |
| `Landmark Detection Model`     |      [shape_predictor_68_face_landmarks.dat](https://github.com/italojs/facial-landmarks-recognition-/blob/master/shape_predictor_68_face_landmarks.dat)|


## Scripts

### GetExpression.cs
```
--Momentum Weight, default=2.0, type=float     # How far in the future to guess value, based on previus two frames (1.0 means no projection into the future)
--Smoothing Weight, default=.8; type=float     # Blend ratio between inference value at this frame, and projected value based on previos two frames
```
### GetHeadTransform.cs
```
--Momentum Weight, default=2.0, type=float     # How far in the future to guess value, based on previus two frames (1.0 means no projection into the future)
--Smoothing Weight, default=.8; type=float     # Blend ratio between inference value at this frame, and projected value based on previos two frames
```
### OpenCVFaceDetection.cs
```
--Detect Ratio, default=1, type=int            # Amount to scale down image before Bounding Box detector
--Cam Id, default=0, type=int                  # ID of camera to run stream from(Front, Back, etc...)
--Fov Zoom, default=1.0, type=float            # FOV Zoom multiplier, high value will shrink FOV use for PnP Solve
--Draw Face Points, default=false, type=bool   # Whether or not to draw points and axis ornament on face
```
### GetFrame.cs
```
--Texture Resolution, default=1024, type=int   # Resolution of video feed texture
```
## Use
- Open `facial-pose-estimation-unity` as a Unity project
- Within the project, open the scene `\Assets\Scenes\SampleScene.unity`
- Press Play, and the face should snap to your face
