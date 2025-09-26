using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PlayerMovementInstaller : IInstaller
{
    [SerializeField] private PlayerMoverView _playerMoverView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterComponent(_playerMoverView);
        builder.RegisterEntryPoint<PlayerMover>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<PlayerMoverMediator>(Lifetime.Singleton);
    }
}
