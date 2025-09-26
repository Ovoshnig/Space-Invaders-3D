using VContainer;
using VContainer.Unity;

public class ShieldCompositionInstaller : IInstaller
{
    public void Install(IContainerBuilder builder) =>
        builder.RegisterEntryPoint<ShieldTileGameStateChangerMediator>(Lifetime.Singleton);
}
