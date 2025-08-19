using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectLifetimeScope : LifetimeScope
{
    [SerializeField] private GameSettings _gameSettings;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.Register<InputActions>(Lifetime.Singleton);

        builder.RegisterInstance(_gameSettings.SceneSettings);
        builder.RegisterInstance(_gameSettings.AudioSettings);
        builder.RegisterInstance(_gameSettings.PlayerSettings);
        builder.RegisterInstance(_gameSettings.InvaderSettings);
        builder.RegisterInstance(_gameSettings.BulletSettings);
    }
}
