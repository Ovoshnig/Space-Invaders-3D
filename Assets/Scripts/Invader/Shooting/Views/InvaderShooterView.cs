using UnityEngine;

public class InvaderShooterView : MonoBehaviour
{
    public void Shoot(InvaderBulletMoverView bulletMoverView) => 
        bulletMoverView.transform.position = transform.position;
}
