using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    [SerializeField] private FieldView _fieldView;
    [SerializeField] private PlayerSoundPlayerView _playerSoundPlayerView;
    [SerializeField] private PlayerMoverView _playerMoverView;
    [SerializeField] private PlayerShooterView _playerShooterView;
    [SerializeField] private BulletMoverView _bulletPrefab;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_fieldView);

        builder.RegisterEntryPoint<PlayerInputHandler>(Lifetime.Singleton).AsSelf();

        ConfigurePlayerSound(builder);
        ConfigurePlayerMovement(builder);
        ConfigurePlayerShooting(builder);

        ConfigureBulletMovement(builder);
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

    private void ConfigureBulletMovement(IContainerBuilder builder)
    {
        GameObject bulletRoot = new("Bullets");
        builder.RegisterComponentInNewPrefab(_bulletPrefab, Lifetime.Transient)
            .UnderTransform(bulletRoot.transform);
    }

    private void Start()
    {

    }
}
