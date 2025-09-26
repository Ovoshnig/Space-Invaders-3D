using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PlayerSpawnInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<PlayerSpawner>(Lifetime.Singleton).AsSelf();
}
