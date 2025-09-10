using Cysharp.Threading.Tasks;
using UnityEngine;

public class UFODestroyerView : CollidedDestroyerView<PlayerBulletMoverView>
{
    [SerializeField] private UFOExplosionView _ufoExplosionView;

    private int _lastPoints = 0;

    public override void Destroy(PlayerBulletMoverView playerBulletView)
    {
        _ufoExplosionView.transform.position = transform.position;
        _ufoExplosionView.ExplodeAsync(_lastPoints).Forget();
    }

    public void SetLastPoints(int value) => _lastPoints = value;
}
