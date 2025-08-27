using R3;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class InvaderColliderView : MonoBehaviour
{
    private readonly Subject<Collider> _collided = new();

    public Observable<Collider> Collided => _collided;

    private void OnTriggerEnter(Collider other) => _collided.OnNext(other);
}
