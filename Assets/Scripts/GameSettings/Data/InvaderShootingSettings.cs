using System;
using UnityEngine;

[Serializable]
public class InvaderShootingSettings
{
    [field: SerializeField, Min(0f)] public float MinDelay { get; private set; } = 1f;
    [field: SerializeField, Min(0f)] public float MaxDelay { get; private set; } = 10f;
    [field: SerializeField, Min(0f)] public int MaxActive { get; private set; } = 3;
}
