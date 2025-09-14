using UnityEngine;

public class BulletDestroyerView : MonoBehaviour
{
    [SerializeField] private BulletExplosionView _explosionViewPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BulletMoverView _))
        {
            Instantiate(_explosionViewPrefab, transform.position, Quaternion.identity);

            gameObject.SetActive(false);
        }
    }
}
