using Cysharp.Threading.Tasks;
using R3;
using System.Threading;
using System.Threading.Tasks;

public readonly struct ShotEvent
{
    public int InvaderIndex { get; }
    public InvaderBulletMoverView Bullet { get; }

    public ShotEvent(int invaderIndex, InvaderBulletMoverView bullet)
    {
        InvaderIndex = invaderIndex;
        Bullet = bullet;
    }
}

public class InvaderShooter
{
    private readonly InvaderBulletPool _bulletPool;
    private readonly InvaderShootingSettings _settings;
    private readonly System.Random _random = new();
    private readonly Subject<ShotEvent> _shot = new();

    private int _invadersCount = 0;

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

            if (_bulletPool.TryGetBullet(out InvaderBulletMoverView bulletMoverView))
            {
                int randomInvaderIndex = _random.Next(0, _invadersCount);
                _shot.OnNext(new ShotEvent(randomInvaderIndex, bulletMoverView));
            }
        }
    }
}
