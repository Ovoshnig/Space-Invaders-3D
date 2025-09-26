using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class InvaderSpawnInstaller : IInstaller
{
    [SerializeField] private InvaderEntityView[] _invaderPrefabs;
    [SerializeField] private InvaderCenterView _invaderCenterView;

    public void Install(IContainerBuilder builder)
    {
        builder.Register<InvaderRegistry>(Lifetime.Singleton);

        builder.Register<InvaderFactory>(Lifetime.Singleton)
            .WithParameter(_invaderPrefabs);
        builder.RegisterEntryPoint<InvaderSpawner>(Lifetime.Singleton)
            .AsSelf();

        builder.RegisterComponent(_invaderCenterView);
    }
}
