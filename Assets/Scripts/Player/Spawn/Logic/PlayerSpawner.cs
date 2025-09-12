using R3;
using System;
using UnityEngine;
using VContainer.Unity;

public class PlayerSpawner : IInitializable, IDisposable
{
    private readonly PlayerDestroyer _playerDestroyer;
    private readonly PlayerMoverView _playerMoverView;
    private readonly InvaderRegistry _invaderRegistry;
    private readonly CompositeDisposable _compositeDisposable = new();

    private Vector3 _startPosition;

    public PlayerSpawner(PlayerDestroyer playerDestroyer,
        PlayerMoverView playerMoverView,
        InvaderRegistry invaderRegistry)
    {
        _playerDestroyer = playerDestroyer;
        _playerMoverView = playerMoverView;
        _invaderRegistry = invaderRegistry;
    }

    public void Initialize()
    {
        _startPosition = _playerMoverView.transform.position;

        _playerDestroyer.Destroyed
            .Subscribe(_ => ReturnToStartPosition())
            .AddTo(_compositeDisposable);

        _invaderRegistry.Any
            .Where(any => !any)
            .Subscribe(_ => ReturnToStartPosition())
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();

    private void ReturnToStartPosition() => _playerMoverView.transform.position = _startPosition;
}
