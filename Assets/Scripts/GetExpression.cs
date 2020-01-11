using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetExpression : MonoBehaviour
{

    int blend_shape_count;
    SkinnedMeshRenderer skinned_mesh_renderer;
    Mesh skinned_mesh;
    public static List<float> prev_blend_weights { get; private set; }

    // Controls for momentum blending
    public float momentumWeight = 2.0f;
    public float smoothingWeight = .8f;

    void Awake()
    {

        skinned_mesh_renderer = GetComponent<SkinnedMeshRenderer>();
        skinned_mesh = GetComponent<SkinnedMeshRenderer>().sharedMesh;
    }

    void Start()
    {
        blend_shape_count = skinned_mesh.blendShapeCount;


    // Storage for previous frame values
    prev_blend_weights = new List<float>(51);
        for (int i = 0; i < 51; i++)
        {
            prev_blend_weights.Add(100);
        }

    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 51; i++)
        {
            // Geuss the next frame using momentum
            float blend_weight_guess = Mathf.LerpUnclamped(prev_blend_weights[i], skinned_mesh_renderer.GetBlendShapeWeight(i), momentumWeight);

            // Set previous frame, get value for current frame
            prev_blend_weights[i] = skinned_mesh_renderer.GetBlendShapeWeight(i);
            float blend_weight = OpenCVFaceDetection.blend_shapes[i];

            // Average the two together
            skinned_mesh_renderer.SetBlendShapeWeight(i, Mathf.LerpUnclamped(blend_weight_guess, blend_weight, smoothingWeight));
        }

    }
}
