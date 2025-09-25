using UnityEngine;
using VContainer;
using VContainer.Unity;

public class IntroLifetimeScope : LifetimeScope
{
    [SerializeField] private Canvas _introCanvas;
    [SerializeField] private FirstLevelButtonView _firstLevelButtonView;

    protected override void Configure(IContainerBuilder builder)
    {
        TextPrinterView[] textPrinterViews = _introCanvas
            .GetComponentsInChildren<TextPrinterView>(includeInactive: true);
        builder.RegisterInstance(textPrinterViews);

        builder.RegisterEntryPoint<MenuInputHandler>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<TextPrinterInputHandlerMediator>(Lifetime.Singleton);

        builder.RegisterInstance(_firstLevelButtonView);
        builder.RegisterEntryPoint<SceneSwitchFirstLevelButtonViewMediator>(Lifetime.Singleton);
    }
}
