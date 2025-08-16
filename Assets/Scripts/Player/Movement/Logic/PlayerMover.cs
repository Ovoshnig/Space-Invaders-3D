using R3;
using System;
using UnityEngine;
using VContainer.Unity;

public class PlayerMover : ITickable, IDisposable
{
    private readonly PlayerInputHandler _playerInputHandler;
    private readonly PlayerSettings _playerSettings;
    private readonly ReactiveProperty<Vector3> _frameMotion = new(Vector3.zero);
    private readonly CompositeDisposable _compositeDisposable = new();

    private float _velocity;
    private bool _isPause = false;

    public PlayerMover(PlayerInputHandler playerState,
        PlayerSettings playerSettings)
    {
        _playerInputHandler = playerState;
        _playerSettings = playerSettings;
    }

    public ReadOnlyReactiveProperty<Vector3> FrameMotion => _frameMotion;

    public void Tick()
    {
        if (_isPause)
            return;

        _velocity = _playerSettings.WalkSpeed * _playerInputHandler.WalkInput.CurrentValue;
        _frameMotion.Value = new Vector3(_velocity, 0f, 0f) * Time.deltaTime;
    }

    public void Dispose() => _compositeDisposable.Dispose();

    public void SetPause(bool value) => _isPause = value;
}
