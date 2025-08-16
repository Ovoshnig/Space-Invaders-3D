using R3;
using UnityEngine;
using VContainer.Unity;

public class PlayerShooter : ITickable
{
    private readonly PlayerInputHandler _playerInputHandler;
    private readonly PlayerSettings _playerSettings;
    private readonly Subject<Unit> _shot = new();

    private float _remainingTime = 0f;

    public PlayerShooter(PlayerInputHandler playerInputHandler,
        PlayerSettings playerSettings)
    {
        _playerInputHandler = playerInputHandler;
        _playerSettings = playerSettings;
    }

    public Observable<Unit> Shot => _shot;

    public void Tick()
    {
        if (_remainingTime > 0f)
        {
            _remainingTime -= Time.deltaTime;
        }
        else
        {
            if (_playerInputHandler.ShootPressed.CurrentValue)
            {
                _shot.OnNext(Unit.Default);
                _remainingTime = _playerSettings.ShootCooldown;
            }
        }
    }
}
