using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

[Serializable]
public class InvaderSoundInstaller : IInstaller
{
    [SerializeField] private InvaderSoundPlayerView _invaderSoundPlayerView;

    public void Install(IContainerBuilder builder)
    {
        builder.RegisterInstance(_invaderSoundPlayerView);
        builder.RegisterEntryPoint<InvaderSoundPlayer>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<InvaderSoundPlayerMediator>(Lifetime.Singleton);
    }
}
