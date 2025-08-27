using Cysharp.Threading.Tasks;
using UnityEngine;

public class InvaderExplosionView : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _duration = 0.5f;

    private async void Start()
    {
        await UniTask.WaitForSeconds(_duration);
        Destroy(gameObject);
    }
}
