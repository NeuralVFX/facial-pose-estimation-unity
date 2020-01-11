using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System;


internal static class OpenCVInterop
{

    [DllImport("facial-pose-estimation-opencv")]
    internal static extern int Init(ref int outCameraWidth, ref int outCameraHeight, int ratio, int camId, float fovZoom, bool draw);

    [DllImport("facial-pose-estimation-opencv")]
    internal static extern int Close();

    [DllImport("facial-pose-estimation-opencv")]
    internal static extern void GetRawImageBytes(IntPtr data, int width, int height);

    [DllImport("facial-pose-estimation-opencv")]
    internal unsafe static extern void Detect(TransformData* outFaces, ExpressionData* outExpression);
}


[StructLayout(LayoutKind.Sequential, Size = 27)]
public struct TransformData
{
    public float tX, tY, tZ, rfX, rfY, rfZ, ruX, ruY, ruZ;
}

[StructLayout(LayoutKind.Sequential, Size = 4)]
public struct ExpressionData
{
    public float blend_weight;
}



public class OpenCVFaceDetection : MonoBehaviour
{
    // Storage for retrieved data
    public static Vector3 trans { get; private set; }
    public static Vector3 rot_u { get; private set; }
    public static Vector3 rot_f { get; private set; }
    public static List<float> blend_shapes { get; private set; }

    // Controls for DLL
    public int detectRatio = 1;
    public int camId;
    public float fovZoom = 1.0f;
    public bool drawFacePoints = false;

    private bool _ready = false;
    private TransformData faces;
    private ExpressionData[] expressions;
    void Start()
    {
        // Initiate Open CV Wrapper
        int cam_width = 1920, cam_height = 1080;
        int result = OpenCVInterop.Init(ref cam_width, ref cam_height, detectRatio, camId, fovZoom, drawFacePoints);

        // Setup camera FOV
        float vfov = 2.0f * Mathf.Atan(0.5f * (cam_height) /( cam_width * fovZoom)) * Mathf.Rad2Deg;
        Camera cam = Camera.main;
        cam.fieldOfView = vfov;
        cam.aspect = (float)cam_width / (float)cam_height;

        // Move background plane to fit camera FOV
        Transform bg_transform;
        bg_transform = this.gameObject.transform.GetChild(0);
        Vector3 bg_depth = new Vector3(0.0f, 0.0f, fovZoom * 18000.0f);
        bg_transform.position = bg_depth;

        // Initiate blendhsapes and transforms
        trans = new Vector3();
        rot_u = new Vector3();
        rot_f = new Vector3();
        expressions = new ExpressionData[51];
        blend_shapes = new List<float>(51);
        for (int i = 0; i < 51; i++)
        {
            blend_shapes.Add(100);
        }

        _ready = true;
    }

    void OnApplicationQuit()
    {
        if (_ready)
        {
            OpenCVInterop.Close();
        }
    }

    void Update()
    {
        if (!_ready)
            return;

        // Get data from DLL
        unsafe
        {
            fixed (ExpressionData* outExpression = expressions)
            {
                fixed (TransformData* outFace = &faces)
                {
                    OpenCVInterop.Detect(outFace, outExpression);
                }
            }
        }

        // Set data
        trans = new Vector3(faces.tX, faces.tY, faces.tZ);
        rot_u = new Vector3(faces.ruX, faces.ruY, faces.ruZ);
        rot_f = new Vector3(faces.rfX, faces.rfY, faces.rfZ);

        for (int i = 0; i < 51; i++)
        {
            blend_shapes[i] = (expressions[i].blend_weight * 100.0f);
        }

    }

    public static void GetFrame(IntPtr pixelPtr, int width, int height)
    {
        OpenCVInterop.GetRawImageBytes(pixelPtr, width, height);
    }
}

