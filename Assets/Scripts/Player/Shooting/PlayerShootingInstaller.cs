using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PlayerShootingInstaller : IInstaller
{
    [SerializeField] private PlayerShooterView _playerShooterView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterComponent(_playerShooterView);
        builder.Register<PlayerShooterModel>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PlayerShooter>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<PlayerShooterMediator>(Lifetime.Singleton);
    }
}
