using System;
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
    }

    private void ConfigurePlayerSound(IContainerBuilder builder)
    {
        builder.RegisterComponent(_playerSoundPlayerView);
        builder.Register<PlayerSoundLoader>(Lifetime.Singleton);
        builder.RegisterEntryPoint(container =>
        {
            PlayerSoundLoader playerSoundLoader = container.Resolve<PlayerSoundLoader>();
            PlayerSoundPlayerView playerSoundPlayerView = container.Resolve<PlayerSoundPlayerView>();
            return new PlayerSoundLoaderSoundPlayerViewMediator(playerSoundLoader, playerSoundPlayerView);
        }, Lifetime.Singleton);

        builder.RegisterEntryPoint(container =>
        {
            PlayerShooter playerShooter = container.Resolve<PlayerShooter>();
            PlayerSoundPlayerView playerSoundPlayerView = container.Resolve<PlayerSoundPlayerView>();
            return new PlayerSoundPlayerShooterMediator(playerShooter, playerSoundPlayerView);
        }, Lifetime.Singleton);
    }

    private void ConfigurePlayerMovement(IContainerBuilder builder)
    {
        builder.RegisterComponent(_playerMoverView);
        builder.RegisterEntryPoint<PlayerMover>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint(container =>
        {
            PlayerMover playerMover = container.Resolve<PlayerMover>();
            PlayerMoverView playerMoverView = container.Resolve<PlayerMoverView>();
            return new PlayerMoverMediator(playerMover, playerMoverView);
        }, Lifetime.Singleton);
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
        builder.Register<InvaderCollider>(Lifetime.Singleton);
        builder.RegisterEntryPoint<InvaderColliderMediator>(Lifetime.Singleton);
    }

    private void ConfigureInvaderDestruction(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<InvaderDestroyer>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<InvaderDestroyerMediator>(Lifetime.Singleton);
    }

    private void ConfigureUFOMovement(IContainerBuilder builder)
    {
        builder.RegisterComponent(_UFOMoverView);
        builder.RegisterEntryPoint<UFOMover>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<UFOMoverMediator>(Lifetime.Singleton);
    }

    private void Start()
    {

    }
}
