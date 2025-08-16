using System;
using UnityEngine;

[Serializable]
public class PlayerSettings
{
    [field: SerializeField, Min(0f)] public float WalkSpeed { get; private set; } = 3.5f;
    [field: SerializeField, Min(0f)] public float ShootCooldown { get; private set; } = 0.4f;
}
