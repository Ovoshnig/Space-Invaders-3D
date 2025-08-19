using System;
using UnityEngine;

[Serializable]
public class InvaderSettings
{
    [field: SerializeField] public GameObject InvaderPrefab { get; private set; }
    [field: SerializeField, Range(0f, 1f)] public float MarginRatioZ { get; private set; } = 0.1f;
    [field: SerializeField, Range(0f, 1f)] public float SpawnRangeRatioX { get; private set; } = 0.7f;
    [field: SerializeField, Range(0f, 1f)] public float SpacingRatioX { get; private set; } = 0.5f;
    [field: SerializeField, Range(0f, 1f)] public float SpacingRatioZ { get; private set; } = 0.8f;
    [field: SerializeField] public float SpawnPositionY { get; private set; } = 0.1f;
    [field: SerializeField] public int[] RowIndices { get; private set; } = { };
    [field: SerializeField, Min(0f)] public float StepX { get; private set; } = 2.5f;
    [field: SerializeField, Min(0f)] public float StepZ { get; private set; } = 5f;
    [field: SerializeField, Min(0f)] public float ShootCooldown { get; private set; } = 0.4f;

    public Bounds InvaderBounds => InvaderPrefab.GetComponentInChildren<MeshRenderer>().bounds;
}
