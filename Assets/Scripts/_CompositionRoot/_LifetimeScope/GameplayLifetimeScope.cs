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
    [SerializeField] private GameObject[] _invaderPrefabs;
    [SerializeField] private InvaderSpawnerView _invaderSpawnerView;

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
        builder.RegisterEntryPoint<PlayerShooter>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint(container =>
        {
            PlayerShooter playerShooter = container.Resolve<PlayerShooter>();
            PlayerShooterView playerShooterView = container.Resolve<PlayerShooterView>();
            return new PlayerShooterMediator(playerShooter, playerShooterView);
        }, Lifetime.Singleton);
    }

    private void ConfigureBulletMovement(IContainerBuilder builder) => 
        builder.RegisterComponent(_playerBulletMoverView);

    private void ConfigureInvaderSpawn(IContainerBuilder builder)
    {
        builder.Register<InvaderRegistry>(Lifetime.Singleton);
        builder.Register<InvaderFactory>(Lifetime.Singleton)
            .WithParameter(_invaderPrefabs);

        builder.RegisterComponent(_invaderSpawnerView);
        builder.Register<InvaderSpawner>(Lifetime.Singleton);
        builder.RegisterEntryPoint(container =>
        {
            InvaderSpawner invaderSpawner = container.Resolve<InvaderSpawner>();
            InvaderSpawnerView invaderSpawnerView = container.Resolve<InvaderSpawnerView>();
            return new InvaderSpawnerMediator(invaderSpawner, invaderSpawnerView);
        }, Lifetime.Singleton);
    }

    private void ConfigureInvaderMovement(IContainerBuilder builder)
    {
        builder.Register<InvaderMover>(Lifetime.Singleton);
        builder.RegisterEntryPoint<InvaderMoverMediator>(Lifetime.Singleton);
    }

    private void Start()
    {

    }
}
