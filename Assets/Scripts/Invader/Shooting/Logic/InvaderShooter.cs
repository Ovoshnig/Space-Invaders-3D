using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;

public record ShotEvent(int InvaderIndex, InvaderBulletMoverView Bullet);

public class InvaderShooter
{
    private readonly InvaderBulletPool _bulletPool;
    private readonly InvaderShootingSettings _settings;
    private readonly Random _random = new();
    private readonly Subject<ShotEvent> _shot = new();

    private int[] _invaderIndices;
    private bool _isPause = false;

    public InvaderShooter(InvaderBulletPool bulletPool,
        InvaderShootingSettings invaderShootingSettings)
    {
        _bulletPool = bulletPool;
        _settings = invaderShootingSettings;
    }

    public Observable<ShotEvent> Shot => _shot;

    public void SetInvaderIndices(int[] indices) => _invaderIndices = indices;

    public async UniTask StartShootingAsync(CancellationToken token)
    {
        while (true)
        {
            float randomDelay = (float)_random.NextDouble()
                * (_settings.MaxDelay - _settings.MinDelay)
                + _settings.MinDelay;

            await UniTask.WaitForSeconds(randomDelay, cancellationToken: token);

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
}
