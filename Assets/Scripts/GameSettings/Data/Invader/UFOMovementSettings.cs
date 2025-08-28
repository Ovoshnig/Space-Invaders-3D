using System;
using UnityEngine;

[Serializable]
public class UFOMovementSettings
{
    [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }
    [field: SerializeField, Min(1)] public int FirstShotCount { get; private set; } = 23;
    [field: SerializeField, Min(1)] public int NextShotCount { get; private set; } = 15;
    [field: SerializeField, Min(0f)] public float Speed { get; private set; } = 5f;

    public Bounds Bounds => MeshRenderer.bounds;
    public Vector3 Extents => Bounds.extents;
}
