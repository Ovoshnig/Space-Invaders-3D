using System;
using UnityEngine;

[Serializable]
public class InvaderSettings
{
    private Bounds _bounds = default;

    [field: SerializeField] public GameObject InvaderPrefab { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float UpMarginRatioZ { get; private set; } = 0.1f;
    [field: SerializeField, Range(0f, 1f)] public float DownMarginRatioZ { get; private set; } = 0.2f;
    [field: SerializeField, Range(0f, 1f)] public float SpawnRangeRatioX { get; private set; } = 0.7f;
    [field: SerializeField, Range(0f, 1f)] public float SpacingRatioX { get; private set; } = 0.5f;
    [field: SerializeField, Range(0f, 1f)] public float SpacingRatioZ { get; private set; } = 0.8f;
    [field: SerializeField] public float SpawnPositionY { get; private set; } = 0.1f;
    [field: SerializeField] public int[] RowIndices { get; private set; } = { };
    [field: SerializeField, Range(0f, 1f)] public float StepRatioX { get; private set; } = 0.1f;
    [field: SerializeField, Range(0f, 1f)] public float StepRatioZ { get; private set; } = 0.8f;
    [field: SerializeField, Min(0f)] public float StepDelay { get; private set; } = 0.5f;
    [field: SerializeField, Min(0f)] public float ShootCooldown { get; private set; } = 0.4f;

    public Bounds Bounds
    {
        get
        {
            if (_bounds == default)
                _bounds = InvaderPrefab.GetComponentInChildren<MeshRenderer>().bounds;

            return _bounds;
        }
    }
    public Vector3 Size => Bounds.size;
    public Vector3 Extents => Bounds.extents;
    public Vector3 Center => Bounds.center;
    public Vector3 Min => Bounds.min;
    public Vector3 Max => Bounds.max;
    public float SpacingX => SpacingRatioX * Size.x;
    public float SpacingZ => SpacingRatioZ * Size.z;
    public float StepX => StepRatioX * Size.x;
    public float StepZ => StepRatioZ * Size.z;
}
