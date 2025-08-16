using System;
using UnityEngine;

[Serializable]
public class BulletSettings
{
    [field: SerializeField, Min(0f)] public float Speed { get; private set; } = 10f;
}
