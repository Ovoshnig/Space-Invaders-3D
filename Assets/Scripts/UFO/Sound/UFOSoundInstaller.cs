using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class UFOSoundInstaller : IInstaller
{
    [SerializeField] private UFOSoundPlayerView _ufoSoundPlayerView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterComponent(_ufoSoundPlayerView);
        builder.RegisterEntryPoint<UFOSoundPlayer>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<UFOSoundPlayerMediator>(Lifetime.Singleton);
    }
}
