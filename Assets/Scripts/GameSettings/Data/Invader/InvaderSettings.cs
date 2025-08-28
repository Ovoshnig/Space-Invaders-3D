using System;
using UnityEngine;

[Serializable]
public class InvaderSettings
{
    [field: SerializeField] public InvaderSpawnSettings InvaderSpawnSettings { get; private set; }
    [field: SerializeField] public InvaderMovementSettings InvaderMovementSettings { get; private set; }
    [field: SerializeField] public InvaderShootingSettings InvaderShootingSettings { get; private set; }
}
