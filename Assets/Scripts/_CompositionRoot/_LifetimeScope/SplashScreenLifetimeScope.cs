using VContainer;
using VContainer.Unity;

public class SplashScreenLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<SplashScreenLogic>(Lifetime.Singleton)
            .AsSelf();
        builder.RegisterEntryPoint<SceneSwitchSplashScreenLogicMediator>(Lifetime.Singleton);
    }
}
