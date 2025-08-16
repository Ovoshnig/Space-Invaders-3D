using System;
using UnityEngine;

[Serializable]
public class PlayerSettings
{
    [field: SerializeField, Min(0f)] public float WalkSpeed { get; private set; } = 3.5f;
}
