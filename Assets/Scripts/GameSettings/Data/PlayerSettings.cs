using System;
using UnityEngine;

[Serializable]
public class PlayerSettings
{
    [field: SerializeField, Min(0f)] public float MovementSpeed { get; private set; } = 3.5f;
    [field: SerializeField, Min(0f)] public float ExplosionDuration { get; private set; } = 1f;
}
