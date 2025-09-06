using System;
using UnityEngine;

[Serializable]
public class InvaderPointsSettings
{
    [field: SerializeField, Min(1f)] public int Invader1Points { get; private set; } = 10;
    [field: SerializeField, Min(1f)] public int Invader2Points { get; private set; } = 20;
    [field: SerializeField, Min(1f)] public int Invader3Points { get; private set; } = 30;
    [field: SerializeField, Min(1f)] public int[] UFOPointsVariants { get; private set; } 
        = { 50, 100, 150, 300 };
}
