using R3;
using UnityEngine;

public class InvaderMoverView : MonoBehaviour
{
    private readonly Subject<Unit> _destroyed = new();

    public Observable<Unit> Destroyed => _destroyed;

    private void OnDestroy() => _destroyed.OnNext(Unit.Default);

    public void Move(Vector3 movement)
    {
        transform.Translate(movement);
    }
}
