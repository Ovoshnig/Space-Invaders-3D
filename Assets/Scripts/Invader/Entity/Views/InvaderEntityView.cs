using System;
using UnityEngine;

public class InvaderEntityView : MonoBehaviour
{
    [field: SerializeField] public InvaderMoverView InvaderMoverView { get; private set; }
    [field: SerializeField] public InvaderShooterView InvaderShooterView { get; private set; }
    [field: SerializeField] public TriggerColliderView InvaderColliderView { get; private set; }
    [field: SerializeField] public InvaderDestroyerView InvaderDestroyerView { get; private set; }
    [field: SerializeField] public InvaderPointsView InvaderPointsView { get; private set; }

    public T Get<T>() where T : MonoBehaviour
    {
        Type type = typeof(T);

        if (type == typeof(InvaderMoverView))
            return InvaderMoverView as T;
        if (type == typeof(InvaderShooterView))
            return InvaderShooterView as T;
        if (type == typeof(TriggerColliderView))
            return InvaderColliderView as T;
        if (type == typeof(InvaderDestroyerView))
            return InvaderDestroyerView as T;
        if (type == typeof(InvaderPointsView))
            return InvaderPointsView as T;
        return null;
    }
}
