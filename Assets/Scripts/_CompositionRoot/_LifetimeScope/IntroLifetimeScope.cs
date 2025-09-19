using UnityEngine;
using VContainer;
using VContainer.Unity;

public class IntroLifetimeScope : LifetimeScope
{
    [SerializeField] private FirstLevelButtonView _firstLevelButtonView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_firstLevelButtonView);
        builder.RegisterEntryPoint<SceneSwitchFirstLevelButtonViewMediator>(Lifetime.Singleton);
    }
}
