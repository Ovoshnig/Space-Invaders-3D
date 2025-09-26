using VContainer;
using VContainer.Unity;

public class PlayerCompositionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<PlayerMoverGamePauserMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PlayerShooterGamePauserMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PlayerBulletMoverGamePauserMediator>(Lifetime.Singleton);

        builder.RegisterEntryPoint<PlayerSpawnerGameStateChangerMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PlayerShooterModelGameStateChangerMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PlayerHealthModelGameStateChangerMediator>(Lifetime.Singleton);
    }
}
