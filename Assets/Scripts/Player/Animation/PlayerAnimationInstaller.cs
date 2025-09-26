using System;
using VContainer;
using VContainer.Unity;

[Serializable]
public class PlayerAnimationInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<PlayerDestroyerAnimatorViewMediator>(Lifetime.Singleton);
}
