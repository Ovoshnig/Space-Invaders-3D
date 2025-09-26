using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class UFODestructionInstaller : IInstaller
{
    [SerializeField] private UFODestroyerView _ufoDestroyerView;
    [SerializeField] private UFOExplosionView _ufoExplosionView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterComponent(_ufoDestroyerView);
        builder.RegisterComponent(_ufoExplosionView);
        builder.RegisterEntryPoint<UFODestroyer>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<UFODestroyerMediator>(Lifetime.Singleton);
    }
}
