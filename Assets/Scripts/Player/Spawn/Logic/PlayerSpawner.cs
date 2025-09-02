using R3;
using System;
using UnityEngine;
using VContainer.Unity;

public class PlayerSpawner : IInitializable, IDisposable
{
    private readonly PlayerDestroyer _playerDestroyer;
    private readonly PlayerMoverView _playerMoverView;
    private readonly CompositeDisposable _compositeDisposable = new();

    public PlayerSpawner(PlayerDestroyer playerDestroyer, PlayerMoverView playerMoverView)
    {
        _playerDestroyer = playerDestroyer;
        _playerMoverView = playerMoverView;
    }

    public void Initialize()
    {
        Vector3 startPosition = _playerMoverView.transform.position;

        _playerDestroyer.Destroyed
            .Subscribe(_ => _playerMoverView.transform.position = startPosition)
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();
}
