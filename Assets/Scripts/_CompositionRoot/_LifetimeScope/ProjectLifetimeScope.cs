using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectLifetimeScope : LifetimeScope
{
    [SerializeField] private GameSettings _gameSettings;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<AddressableLoader>(Lifetime.Singleton);
        builder.Register<InputActions>(Lifetime.Singleton);

        builder.RegisterEntryPoint<SceneSwitch>(Lifetime.Singleton)
            .AsSelf();

        builder.RegisterInstance(_gameSettings.SceneSettings);
        builder.RegisterInstance(_gameSettings.AudioSettings);
        builder.RegisterInstance(_gameSettings.PlayerSettings);
        builder.RegisterInstance(_gameSettings.BulletSettings);

        builder.RegisterInstance(_gameSettings.InvaderSettings.InvaderSpawnSettings);
        builder.RegisterInstance(_gameSettings.InvaderSettings.InvaderMovementSettings);
        builder.RegisterInstance(_gameSettings.InvaderSettings.InvaderShootingSettings);
        builder.RegisterInstance(_gameSettings.InvaderSettings.InvaderPointsSettings);

        builder.RegisterInstance(_gameSettings.UFOMovementSettings);
    }
}
