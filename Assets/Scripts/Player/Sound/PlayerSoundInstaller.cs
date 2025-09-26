using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PlayerSoundInstaller : IInstaller
{
    [SerializeField] private PlayerSoundPlayerView _playerSoundPlayerView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterComponent(_playerSoundPlayerView);
        builder.RegisterEntryPoint<PlayerSoundPlayer>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<PlayerSoundPlayerMediator>(Lifetime.Singleton);
    }
}
