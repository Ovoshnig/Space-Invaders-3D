using UnityEngine;

public class PlayerShooterView : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;

    public void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, transform);
        bullet.transform.SetParent(null);
    }
}
