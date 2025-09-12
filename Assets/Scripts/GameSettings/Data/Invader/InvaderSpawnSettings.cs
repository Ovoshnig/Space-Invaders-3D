using System;
using UnityEngine;

[Serializable]
public class InvaderSpawnSettings
{
    [field: SerializeField] public MeshRenderer MeshRenderer { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float UpMarginRatioZ { get; private set; } = 0.1f;
    [field: SerializeField, Range(0f, 1f)] public float DownMarginRatioZ { get; private set; } = 0.2f;
    [field: SerializeField, Range(0f, 1f)] public float SpawnRangeRatioX { get; private set; } = 0.7f;
    [field: SerializeField, Range(0f, 1f)] public float SpacingRatioX { get; private set; } = 0.5f;
    [field: SerializeField, Range(0f, 1f)] public float SpacingRatioZ { get; private set; } = 0.8f;
    [field: SerializeField] public float SpawnPositionY { get; private set; } = 0.1f;
    [field: SerializeField] public int[] RowIndices { get; private set; } = { };
    [field: SerializeField, Min(0f)] public float Delay { get; private set; } = 0.05f;

    public Bounds Bounds => MeshRenderer.bounds;
    public Vector3 Size => Bounds.size;
    public Vector3 Extents => Bounds.extents;
    public Vector3 Center => Bounds.center;
    public Vector3 Min => Bounds.min;
    public Vector3 Max => Bounds.max;
    public float SpacingX => SpacingRatioX * Size.x;
    public float SpacingZ => SpacingRatioZ * Size.z;
}
