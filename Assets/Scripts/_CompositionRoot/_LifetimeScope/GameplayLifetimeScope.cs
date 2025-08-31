using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    [SerializeField] private FieldView _fieldView;
    [SerializeField] private PlayerSoundPlayerView _playerSoundPlayerView;
    [SerializeField] private PlayerMoverView _playerMoverView;
    [SerializeField] private PlayerShooterView _playerShooterView;
    [SerializeField] private PlayerBulletMoverView _playerBulletMoverView;
    [SerializeField] private InvaderEntityView[] _invaderPrefabs;
    [SerializeField] private InvaderBulletMoverView[] _invaderBulletPrefabs;
    [SerializeField] private UFOMoverView _UFOMoverView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_fieldView);

        builder.RegisterEntryPoint<PlayerInputHandler>(Lifetime.Singleton).AsSelf();

        ConfigurePlayerSound(builder);
        ConfigurePlayerMovement(builder);
        ConfigurePlayerShooting(builder);

        ConfigureBulletMovement(builder);

        ConfigureInvaderSpawn(builder);
        ConfigureInvaderMovement(builder);
        ConfigureInvaderShooting(builder);
        ConfigureInvaderCollision(builder);
        ConfigureInvaderDestruction(builder);

        ConfigureUFOMovement(builder);
        ConfigureUFOCollision(builder);
        ConfigureUFODestruction(builder);
    }

    private void ConfigurePlayerSound(IContainerBuilder builder)
    {
        builder.RegisterComponent(_playerSoundPlayerView);
        builder.Register<PlayerSoundLoader>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PlayerSoundLoaderSoundPlayerViewMediator>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PlayerSoundPlayerShooterMediator>(Lifetime.Singleton);
    }

    private void ConfigurePlayerMovement(IContainerBuilder builder)
    {
        builder.RegisterComponent(_playerMoverView);
        builder.RegisterEntryPoint<PlayerMover>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<PlayerMoverMediator>(Lifetime.Singleton);
    }

    private void ConfigurePlayerShooting(IContainerBuilder builder)
    {
        builder.RegisterComponent(_playerShooterView);
        builder.Register<PlayerShooterModel>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PlayerShooter>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<PlayerShooterMediator>(Lifetime.Singleton);
    }

    private void ConfigureBulletMovement(IContainerBuilder builder) =>
        builder.RegisterComponent(_playerBulletMoverView);

    private void ConfigureInvaderSpawn(IContainerBuilder builder)
    {
        builder.Register<InvaderRegistry>(Lifetime.Singleton);

        builder.RegisterEntryPoint<InvaderFactory>(Lifetime.Singleton)
            .WithParameter(_invaderPrefabs)
            .AsSelf();
        builder.RegisterEntryPoint<InvaderSpawner>(Lifetime.Singleton);
    }

    private void ConfigureInvaderMovement(IContainerBuilder builder)
    {
        builder.Register<InvaderMover>(Lifetime.Singleton);
        builder.RegisterEntryPoint<InvaderMoverMediator>(Lifetime.Singleton);
    }

    private void ConfigureInvaderShooting(IContainerBuilder builder)
    {
        builder.RegisterInstance(_invaderBulletPrefabs);
        builder.RegisterEntryPoint<InvaderBulletPool>(Lifetime.Singleton).AsSelf();

        builder.Register<InvaderShooter>(Lifetime.Singleton);
        builder.RegisterEntryPoint<InvaderShooterMediator>(Lifetime.Singleton);
    }

    private void ConfigureInvaderCollision(IContainerBuilder builder)
    {
        builder.Register<CollisionReporter<InvaderEntityView>>(Lifetime.Singleton);
        builder.RegisterEntryPoint<InvaderCollisionMediator>(Lifetime.Singleton);
    }

    private void ConfigureInvaderDestruction(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<CollidedDestroyer<InvaderEntityView, PlayerBulletMoverView>>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<InvaderDestroyerMediator>(Lifetime.Singleton);
    }

    private void ConfigureUFOMovement(IContainerBuilder builder)
    {
        builder.RegisterComponent(_UFOMoverView);
        builder.RegisterEntryPoint<UFOMover>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<UFOMoverMediator>(Lifetime.Singleton);
    }

    private void ConfigureUFOCollision(IContainerBuilder builder)
    {
        builder.Register<CollisionReporter<UFOMoverView>>(Lifetime.Singleton);
        builder.RegisterEntryPoint<UFOCollisionMediator>(Lifetime.Singleton);
    }

    private void ConfigureUFODestruction(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<CollidedDestroyer<UFOMoverView, PlayerBulletMoverView>>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<UFODestroyerMediator>(Lifetime.Singleton);
    }

    private void Start()
    {

    }
}
