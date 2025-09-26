using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class GameSettingsInstaller : IInstaller
{
    [SerializeField] private GameSettings _gameSettings;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_gameSettings.SceneSettings);
        builder.RegisterInstance(_gameSettings.FieldSettings);

        builder.RegisterInstance(_gameSettings.PlayerSettings);

        builder.RegisterInstance(_gameSettings.InvaderSettings.InvaderSpawnSettings);
        builder.RegisterInstance(_gameSettings.InvaderSettings.InvaderMovementSettings);
        builder.RegisterInstance(_gameSettings.InvaderSettings.InvaderShootingSettings);
        builder.RegisterInstance(_gameSettings.InvaderSettings.InvaderPointsSettings);

        builder.RegisterInstance(_gameSettings.UFOMovementSettings);

        builder.RegisterInstance(_gameSettings.AudioSettings);
    }
}
