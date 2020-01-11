
using UnityEngine;


public class GetHeadTransform : MonoBehaviour
{
    // Storage for previos frame transform
    Quaternion prev_rotation;
    Vector3 prev_position;

    // Controls for momentum blending
    public float momentumWeight = 2.0f;
    public float smoothingWeight = .8f;

    void Start()
    {

    }

    void Update()
    {
        // Use velocity to make transform geuss
        Vector3 position_guess = Vector3.LerpUnclamped(prev_position, transform.position, momentumWeight);
        Quaternion rotation_guess = Quaternion.LerpUnclamped(prev_rotation, transform.rotation, momentumWeight);

        prev_rotation = transform.rotation;
        prev_position = transform.position;

        // Buid transform from OpenCV
        Quaternion rotation = Quaternion.LookRotation(new Vector3(OpenCVFaceDetection.rot_f.x, OpenCVFaceDetection.rot_f.y, OpenCVFaceDetection.rot_f.z),
                             new Vector3(-OpenCVFaceDetection.rot_u.x, OpenCVFaceDetection.rot_u.y, OpenCVFaceDetection.rot_u.z));
        Vector3 trans = new Vector3(OpenCVFaceDetection.trans.x, -OpenCVFaceDetection.trans.y, OpenCVFaceDetection.trans.z);

        // Average the two together
        transform.position = Vector3.Lerp(position_guess, trans, smoothingWeight);
        transform.rotation = Quaternion.Lerp(rotation_guess, rotation, smoothingWeight);

    }
}