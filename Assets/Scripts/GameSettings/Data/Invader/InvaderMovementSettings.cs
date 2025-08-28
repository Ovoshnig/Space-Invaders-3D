using System;
using UnityEngine;

[Serializable]
public class InvaderMovementSettings
{
    [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float StepRatioX { get; private set; } = 0.1f;
    [field: SerializeField, Range(0f, 1f)] public float StepRatioZ { get; private set; } = 0.8f;
    [field: SerializeField, Min(0f)] public float StepDelay { get; private set; } = 0.5f;

    public Bounds Bounds => MeshRenderer.bounds;
    public Vector3 Size => Bounds.size;
    public float StepX => StepRatioX * Size.x;
    public float StepZ => StepRatioZ * Size.z;
}
