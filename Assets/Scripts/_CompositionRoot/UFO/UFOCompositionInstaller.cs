using VContainer;
using VContainer.Unity;

public class UFOCompositionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<UFOMoverGamePauserMediator>(Lifetime.Singleton);

        builder.RegisterEntryPoint<UFOMoverGameStateChangerMediator>(Lifetime.Singleton);
    }
}
