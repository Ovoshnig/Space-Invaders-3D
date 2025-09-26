using VContainer;
using VContainer.Unity;

public class InvaderCompositionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<InvaderMoverGamePauserMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<InvaderShooterGamePauserMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<InvaderBulletPoolGamePauserMediator>(Lifetime.Singleton);

        builder.RegisterEntryPoint<InvaderRegistryGameStateChangerMediator>(Lifetime.Singleton);
    }
}
