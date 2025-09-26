using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class UFOMovementInstaller : IInstaller
{
    [SerializeField] private UFOMoverView _UFOMoverView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterComponent(_UFOMoverView);
        builder.RegisterEntryPoint<UFOMover>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<UFOMoverMediator>(Lifetime.Singleton);
    }
}
