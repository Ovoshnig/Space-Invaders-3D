using R3;
using VContainer.Unity;

public class PlayerShooter : ITickable
{
    private readonly PlayerInputHandler _playerInputHandler;
    private readonly Subject<Unit> _shot = new();

    private bool _isBulletEnabled = false;
    private bool _isPause = false;

    public PlayerShooter(PlayerInputHandler playerInputHandler) => _playerInputHandler = playerInputHandler;

    public Observable<Unit> Shot => _shot;

    public void Tick()
    {
        if (_playerInputHandler.ShootPressed.CurrentValue && !_isBulletEnabled && !_isPause)
        {
            _shot.OnNext(Unit.Default);
            SetBulletEnabled(true);
        }
    }

    public void SetBulletEnabled(bool value) => _isBulletEnabled = value;

    public void SetPause(bool value) => _isPause = value;
}
