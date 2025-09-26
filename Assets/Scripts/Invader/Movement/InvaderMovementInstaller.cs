using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class InvaderMovementInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.Register<InvaderMover>(Lifetime.Singleton);
        builder.RegisterEntryPoint<InvaderMoverMediator>(Lifetime.Singleton);
    }
}
