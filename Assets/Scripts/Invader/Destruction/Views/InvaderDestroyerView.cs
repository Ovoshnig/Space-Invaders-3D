using UnityEngine;

public class InvaderDestroyerView : MonoBehaviour
{
    [SerializeField] private InvaderExplosionView _explosionView;

    public void Destroy() => Instantiate(_explosionView, transform.position, Quaternion.identity);
}
