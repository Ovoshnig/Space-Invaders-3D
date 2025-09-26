using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PlayerCollisionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.Register<PlayerCollisionReporter>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PlayerCollisionMediator>(Lifetime.Singleton);
    }
}
