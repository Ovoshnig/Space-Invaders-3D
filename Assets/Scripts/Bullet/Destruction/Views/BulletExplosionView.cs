using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

public class BulletExplosionView : MonoBehaviour
{
    [SerializeField, Min(0f)] private float _duration = 0.7f;

    private readonly CancellationTokenSource _cts = new();

    private void Start() => Explode().Forget();

    private void OnDestroy()
    {
        _cts.Cancel();
        _cts.Dispose();
    }

    private async UniTask Explode()
    {
        await UniTask.WaitForSeconds(_duration, cancellationToken: _cts.Token);
        Destroy(gameObject);
    }
}
