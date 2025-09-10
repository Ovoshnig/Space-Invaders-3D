using Cysharp.Threading.Tasks;
using R3;
using System;
using System.Threading;
using System.Threading.Tasks;

public record ShotEvent(int InvaderIndex, InvaderBulletMoverView Bullet);

public class InvaderShooter
{
    private readonly InvaderBulletPool _bulletPool;
    private readonly InvaderShootingSettings _settings;
    private readonly Random _random = new();
    private readonly Subject<ShotEvent> _shot = new();

    private int _invadersCount = 0;
    private bool _isPause = false;

    public InvaderShooter(InvaderBulletPool bulletPool,
        InvaderShootingSettings invaderShootingSettings)
    {
        _bulletPool = bulletPool;
        _settings = invaderShootingSettings;
    }

    public Observable<ShotEvent> Shot => _shot;

    public void SetInvadersCount(int value) => _invadersCount = value;

    public async Task StartShootingAsync(CancellationToken token)
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
                int randomInvaderIndex = _random.Next(0, _invadersCount);
                _shot.OnNext(new ShotEvent(randomInvaderIndex, bulletMoverView));
            }
        }
    }

    public void SetPause(bool value) => _isPause = value;
}
