using System;
using UnityEngine;

[Serializable]
public class PlayerSettings
{
    [field: SerializeField, Min(0f)] public float MovementSpeed { get; private set; } = 3.5f;
    [field: SerializeField, Min(0f)] public float ExplosionDuration { get; private set; } = 1f;
    [field: SerializeField, Min(1f)] public int InitialHealth { get; private set; } = 3;
}
