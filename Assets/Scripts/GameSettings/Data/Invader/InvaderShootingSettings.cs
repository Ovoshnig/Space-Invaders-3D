using System;
using UnityEngine;

[Serializable]
public class InvaderShootingSettings
{
    [field: SerializeField, Min(0f)] public float MinDelay { get; private set; } = 0.5f;
    [field: SerializeField, Min(0f)] public float MaxDelay { get; private set; } = 2.5f;
    [field: SerializeField, Min(0f)] public int MaxActive { get; private set; } = 3;
}
