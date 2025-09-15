using Cysharp.Threading.Tasks;
using R3;
using System.Threading;
using UnityEngine;
using Random = System.Random;

public record ShotEvent(int InvaderIndex, InvaderBulletMoverView Bullet);

public class InvaderShooter
{
    private readonly InvaderBulletPool _bulletPool;
    private readonly InvaderShootingSettings _shootingSettings;
    private readonly InvaderSpawnSettings _spawnSettings;
    private readonly Random _random = new();
    private readonly Subject<ShotEvent> _shot = new();

    private int[] _invaderIndices;
    private float _delay = 1f;
    private bool _isPause = false;

    public InvaderShooter(InvaderBulletPool bulletPool,
        InvaderShootingSettings shootingSettings,
        InvaderSpawnSettings spawnSettings)
    {
        _bulletPool = bulletPool;
        _shootingSettings = shootingSettings;
        _spawnSettings = spawnSettings;
    }

    public Observable<ShotEvent> Shot => _shot;

    public void SetInvaderIndices(int[] indices, int currentInvaderCount)
    {
        _invaderIndices = indices;
        CalculateDelay(currentInvaderCount);
    }

    public async UniTask StartShootingAsync(CancellationToken token)
    {
        while (true)
        {
            await UniTask.WaitForSeconds(_delay, cancellationToken: token);

            if (_isPause)
                await UniTask.WaitWhile(() => _isPause, cancellationToken: token);

            if (_bulletPool.TryGetBullet(out InvaderBulletMoverView bulletMoverView))
            {
                int randomInvaderIndex = _invaderIndices[_random.Next(_invaderIndices.Length)];
                _shot.OnNext(new ShotEvent(randomInvaderIndex, bulletMoverView));
            }
        }
    }

    public void SetPause(bool value) => _isPause = value;

    private void CalculateDelay(int currentInvaderCount)
    {
        float percentageLeft = (float)currentInvaderCount / _spawnSettings.InitialCount;

        _delay = Mathf.Lerp(_shootingSettings.EndDelay,
            _shootingSettings.StartDelay,
            percentageLeft);
    }
}
