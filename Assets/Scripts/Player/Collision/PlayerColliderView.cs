using R3;
using UnityEngine;

public class PlayerColliderView : MonoBehaviour
{
    private readonly Subject<Unit> _collided = new();

    public Observable<Unit> Collided => _collided;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<InvaderBulletMoverView>() != null)
        {
            other.gameObject.SetActive(false);
            _collided.OnNext(Unit.Default);
        }
    }
}
