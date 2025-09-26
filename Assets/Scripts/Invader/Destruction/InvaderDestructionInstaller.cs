using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class InvaderDestructionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<InvaderDestroyer>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<InvaderDestroyerMediator>(Lifetime.Singleton);
    }
}
