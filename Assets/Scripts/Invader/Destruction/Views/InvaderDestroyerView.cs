using R3;
using UnityEngine;

public class InvaderDestroyerView : MonoBehaviour
{
    private readonly Subject<Unit> _destroyed = new();

    public Observable<Unit> Destroyed => _destroyed;

    private void OnDestroy() => _destroyed.OnNext(Unit.Default);
}
