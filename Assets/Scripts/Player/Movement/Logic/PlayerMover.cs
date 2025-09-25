using R3;
using UnityEngine;
using VContainer.Unity;

public class PlayerMover : ITickable
{
    private readonly PlayerInputHandler _playerInputHandler;
    private readonly PlayerSettings _playerSettings;
    private readonly ReactiveProperty<Vector3> _frameMovement = new(Vector3.zero);

    private bool _isPause = false;

    public PlayerMover(PlayerInputHandler playerState,
        PlayerSettings playerSettings)
    {
        _playerInputHandler = playerState;
        _playerSettings = playerSettings;
    }

    public ReadOnlyReactiveProperty<Vector3> FrameMovement => _frameMovement;

    public void Tick()
    {
        if (_isPause)
            return;

        float velocity = _playerSettings.MovementSpeed * _playerInputHandler.WalkInput.CurrentValue;
        _frameMovement.Value = Time.deltaTime * new Vector3(velocity, 0f, 0f);
    }

    public void SetPause(bool value) => _isPause = value;
}
