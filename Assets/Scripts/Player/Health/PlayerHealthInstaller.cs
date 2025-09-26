using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PlayerHealthInstaller : IInstaller
{
    [SerializeField] private PlayerHealthView _playerHealthView;

    public void Install(IContainerBuilder builder)
    {
        builder.Register<PlayerHealthModel>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PlayerHealthLogic>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterComponent(_playerHealthView);
        builder.RegisterEntryPoint<PlayerHealthMediator>(Lifetime.Singleton);
    }
}
