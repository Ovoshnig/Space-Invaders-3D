using System;
using UnityEngine;

[Serializable]
public class FieldSettings
{
    [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }

    public Bounds Bounds => MeshRenderer.bounds;
    public Vector3 Size => Bounds.size;
    public Vector3 Min => Bounds.min;
    public Vector3 Max => Bounds.max;
}
