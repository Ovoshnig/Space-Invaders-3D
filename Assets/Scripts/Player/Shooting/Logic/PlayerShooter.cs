using R3;
using VContainer.Unity;

public class PlayerShooter : ITickable
{
    private readonly PlayerInputHandler _playerInputHandler;
    private readonly PlayerShooterModel _playerShooterModel;
    private readonly Subject<Unit> _shot = new();

    private bool _isBulletEnabled = false;
    private bool _isPause = false;

    public PlayerShooter(PlayerInputHandler playerInputHandler, 
        PlayerShooterModel playerShooterModel)
    {
        _playerInputHandler = playerInputHandler;
        _playerShooterModel = playerShooterModel;
    }

    public Observable<Unit> Shot => _shot;

    public void Tick()
    {
        if (_playerInputHandler.ShootPressed.CurrentValue && !_isBulletEnabled && !_isPause)
        {
            SetBulletEnabled(true);
            _playerShooterModel.Increment();
            _shot.OnNext(Unit.Default);
        }
    }

    public void SetBulletEnabled(bool value) => _isBulletEnabled = value;

    public void SetPause(bool value) => _isPause = value;
}
