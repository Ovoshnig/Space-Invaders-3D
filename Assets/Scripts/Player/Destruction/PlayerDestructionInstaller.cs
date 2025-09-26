using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PlayerDestructionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<PlayerDestroyer>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<PlayerDestroyerMediator>(Lifetime.Singleton);
    }
}
