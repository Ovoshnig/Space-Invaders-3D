using R3;
using System;
using UnityEngine;
using VContainer.Unity;

public class PlayerSpawner : IInitializable, IDisposable
{
    private readonly PlayerMoverView _playerMoverView;
    private readonly PlayerHealthModel _playerHealthModel;
    private readonly CompositeDisposable _compositeDisposable = new();

    private Vector3 _startPosition;

    public PlayerSpawner(PlayerMoverView playerMoverView,
        PlayerHealthModel playerHealthModel)
    {
        _playerMoverView = playerMoverView;
        _playerHealthModel = playerHealthModel;
    }

    public void Initialize()
    {
        _startPosition = _playerMoverView.transform.position;

        _playerHealthModel.Health
            .Where(health => health > 0)
            .Subscribe(_ => ReturnToStartPosition())
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();

    public void ReturnToStartPosition() => _playerMoverView.transform.position = _startPosition;
}
