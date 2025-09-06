using System;
using System.Collections.Generic;
using UnityEngine;

public class InvaderEntityView : MonoBehaviour
{
    private Dictionary<Type, MonoBehaviour> _viewsMap = null;

    [field: SerializeField] public InvaderMoverView InvaderMoverView { get; private set; }
    [field: SerializeField] public InvaderShooterView InvaderShooterView { get; private set; }
    [field: SerializeField] public TriggerColliderView InvaderColliderView { get; private set; }
    [field: SerializeField] public InvaderDestroyerView InvaderDestroyerView { get; private set; }
    [field: SerializeField] public InvaderPointsView InvaderPointsView { get; private set; }

    private Dictionary<Type, MonoBehaviour> ViewsMap
    {
        get
        {
            _viewsMap ??= new Dictionary<Type, MonoBehaviour>
            {
                { typeof(InvaderMoverView), InvaderMoverView },
                { typeof(InvaderShooterView), InvaderShooterView },
                { typeof(TriggerColliderView), InvaderColliderView },
                { typeof(InvaderDestroyerView), InvaderDestroyerView },
                { typeof(InvaderPointsView), InvaderPointsView },
            };

            return _viewsMap;
        }
    }

    public T Get<T>() where T : MonoBehaviour
    {
        if (ViewsMap.TryGetValue(typeof(T), out MonoBehaviour view))
            return view as T;

        Debug.LogWarning($"View of type {typeof(T)} not found.");
        return null;
    }
}
