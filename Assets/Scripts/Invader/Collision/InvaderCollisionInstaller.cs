using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class InvaderCollisionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.Register<InvaderCollisionReporter>(Lifetime.Singleton);
        builder.RegisterEntryPoint<InvaderCollisionMediator>(Lifetime.Singleton);
    }
}
