using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class UFOCollisionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.Register<UFOCollisionReporter>(Lifetime.Singleton);
        builder.RegisterEntryPoint<UFOCollisionMediator>(Lifetime.Singleton);
    }
}
