using System;
using UnityEngine;

[Serializable]
public class InvaderShootingSettings
{
    [field: SerializeField, Min(0f)] public float StartDelay { get; private set; } = 0.5f;
    [field: SerializeField, Min(0f)] public float EndDelay { get; private set; } = 2f;
    [field: SerializeField, Min(0f)] public int MaxActive { get; private set; } = 3;
    [field: SerializeField, Min(0f)] public float BulletSpeed { get; private set; } = 10f;
}
