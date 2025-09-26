﻿using VContainer;
using VContainer.Unity;

public class CameraCompositionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<CameraNoiseViewPlayerDestroyerMediator>(Lifetime.Singleton);
}
