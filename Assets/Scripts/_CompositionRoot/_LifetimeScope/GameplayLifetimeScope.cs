using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    [SerializeField] private ScoreView _scoreView;
    [SerializeField] private GameOverView _gameOverView;
    [SerializeField] private GameRestarterView _gameRestarterView;
    [SerializeField] private PlayerMoverView _playerMoverView;
    [SerializeField] private PlayerShooterView _playerShooterView;
    [SerializeField] private PlayerBulletMoverView _playerBulletMoverView;
    [SerializeField] private PlayerSoundPlayerView _playerSoundPlayerView;
    [SerializeField] private PlayerHealthView _playerHealthView;
    [SerializeField] private InvaderEntityView[] _invaderPrefabs;
    [SerializeField] private InvaderBulletMoverView[] _invaderBulletPrefabs;
    [SerializeField] private InvaderSoundPlayerView _invaderSoundPlayerView;
    [SerializeField] private UFOMoverView _UFOMoverView;
    [SerializeField] private UFOSoundPlayerView _ufoSoundPlayerView;
    [SerializeField] private UFODestroyerView _ufoDestroyerView;
    [SerializeField] private GameObject _shieldParent;

    protected override void Configure(IContainerBuilder builder)
    {
        ConfigureCompositionRoot(builder);

        builder.Register<GamePauser>(Lifetime.Singleton);

        builder.RegisterEntryPoint<PlayerInputHandler>(Lifetime.Singleton).AsSelf();

        ConfigureScore(builder);
        ConfigureGameOver(builder);

        ConfigurePlayerSpawn(builder);
        ConfigurePlayerMovement(builder);
        ConfigurePlayerShooting(builder);
        ConfigurePlayerCollision(builder);
        ConfigurePlayerDestruction(builder);
        ConfigurePlayerHealth(builder);
        ConfigurePlayerAnimation(builder);
        ConfigurePlayerSound(builder);

        ConfigureBulletMovement(builder);

        ConfigureInvaderSpawn(builder);
        ConfigureInvaderMovement(builder);
        ConfigureInvaderShooting(builder);
        ConfigureInvaderCollision(builder);
        ConfigureInvaderDestruction(builder);
        ConfigureInvaderSound(builder);

        ConfigureUFOMovement(builder);
        ConfigureUFOCollision(builder);
        ConfigureUFODestruction(builder);
        ConfigureUFOSound(builder);

        ConfigureShieldTiles(builder);
    }

    private void ConfigureCompositionRoot(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<GamePauserPlayerDestroyerMediator>();
        builder.RegisterEntryPoint<GamePauserInvaderSpawnerMediator>();
        builder.RegisterEntryPoint<GamePauserGameOverMediator>();

        builder.RegisterEntryPoint<PlayerMoverGamePauserMediator>();
        builder.RegisterEntryPoint<PlayerShooterGamePauserMediator>();
        builder.RegisterEntryPoint<PlayerBulletMoverGamePauserMediator>();
        builder.RegisterEntryPoint<InvaderMoverGamePauserMediator>();
        builder.RegisterEntryPoint<InvaderShooterGamePauserMediator>();
        builder.RegisterEntryPoint<InvaderBulletPoolGamePauserMediator>();
        builder.RegisterEntryPoint<UFOMoverGamePauserMediator>();
    }

    private void ConfigureScore(IContainerBuilder builder)
    {
        builder.RegisterComponent(_scoreView);
        builder.RegisterEntryPoint<ScoreLogic>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<ScoreMediator>(Lifetime.Singleton);
    }

    private void ConfigureGameOver(IContainerBuilder builder)
    {
        builder.RegisterComponent(_gameOverView);
        builder.RegisterEntryPoint<GameOver>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<GameOverMediator>(Lifetime.Singleton);

        builder.RegisterComponent(_gameRestarterView);
        builder.Register<GameRestarter>(Lifetime.Singleton);
        builder.RegisterEntryPoint<GameRestarterMediator>(Lifetime.Singleton);
    }

    private void ConfigurePlayerSpawn(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<PlayerSpawner>(Lifetime.Singleton);

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

    private void ConfigurePlayerCollision(IContainerBuilder builder)
    {
        builder.Register<CollisionReporter<PlayerMoverView>>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PlayerCollisionMediator>(Lifetime.Singleton);
    }

    private void ConfigurePlayerDestruction(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<PlayerDestroyer>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<PlayerDestroyerMediator>(Lifetime.Singleton);
    }

    private void ConfigurePlayerHealth(IContainerBuilder builder)
    {
        builder.Register<PlayerHealthModel>(Lifetime.Singleton);
        builder.RegisterEntryPoint<PlayerHealthLogic>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterComponent(_playerHealthView);
        builder.RegisterEntryPoint<PlayerHealthMediator>(Lifetime.Singleton);
    }

    private void ConfigurePlayerAnimation(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<PlayerDestroyerAnimatorViewMediator>(Lifetime.Singleton);

    private void ConfigurePlayerSound(IContainerBuilder builder)
    {
        builder.RegisterComponent(_playerSoundPlayerView);
        builder.RegisterEntryPoint<PlayerSoundPlayer>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<PlayerSoundPlayerMediator>(Lifetime.Singleton);
    }

    private void ConfigureBulletMovement(IContainerBuilder builder) =>
        builder.RegisterComponent(_playerBulletMoverView);

    private void ConfigureInvaderSpawn(IContainerBuilder builder)
    {
        builder.Register<InvaderRegistry>(Lifetime.Singleton);

        builder.Register<InvaderFactory>(Lifetime.Singleton)
            .WithParameter(_invaderPrefabs);
        builder.RegisterEntryPoint<InvaderSpawner>(Lifetime.Singleton)
            .AsSelf();
    }

    private void ConfigureInvaderMovement(IContainerBuilder builder)
    {
        builder.Register<InvaderMover>(Lifetime.Singleton);
        builder.RegisterEntryPoint<InvaderMoverMediator>(Lifetime.Singleton);
    }

    private void ConfigureInvaderShooting(IContainerBuilder builder)
    {
        builder.RegisterInstance(_invaderBulletPrefabs);
        builder.Register<InvaderBulletPool>(Lifetime.Singleton);

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
        builder.RegisterEntryPoint<InvaderDestroyer>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<InvaderDestroyerMediator>(Lifetime.Singleton);
    }

    private void ConfigureInvaderSound(IContainerBuilder builder)
    {
        builder.RegisterComponent(_invaderSoundPlayerView);
        builder.RegisterEntryPoint<InvaderSoundPlayer>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<InvaderSoundPlayerMediator>(Lifetime.Singleton);
    }

    private void ConfigureUFOMovement(IContainerBuilder builder)
    {
        builder.RegisterComponent(_UFOMoverView);
        builder.RegisterEntryPoint<UFOMover>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<UFOMoverMediator>(Lifetime.Singleton);
    }

    private void ConfigureUFOCollision(IContainerBuilder builder)
    {
        builder.Register<CollisionReporter<UFOMoverView>>(Lifetime.Singleton);
        builder.RegisterEntryPoint<UFOCollisionMediator>(Lifetime.Singleton);
    }

    private void ConfigureUFODestruction(IContainerBuilder builder)
    {
        builder.RegisterComponent(_ufoDestroyerView);
        builder.RegisterEntryPoint<UFODestroyer>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<UFODestroyerMediator>(Lifetime.Singleton);
    }

    private void ConfigureUFOSound(IContainerBuilder builder)
    {
        builder.RegisterComponent(_ufoSoundPlayerView);
        builder.RegisterEntryPoint<UFOSoundPlayer>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<UFOSoundPlayerMediator>(Lifetime.Singleton);
    }

    private void ConfigureShieldTiles(IContainerBuilder builder)
    {
        ShieldTileView[] tiles = _shieldParent.GetComponentsInChildren<ShieldTileView>();
        builder.RegisterComponent(tiles);

        builder.RegisterEntryPoint<ShieldTileMediator>(Lifetime.Singleton);
    }

    private void Start()
    {

    }
}
