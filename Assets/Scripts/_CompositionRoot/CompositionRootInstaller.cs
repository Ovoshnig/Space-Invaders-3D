using VContainer;
using VContainer.Unity;

public class CompositionRootInstaller : IInstaller
{
    public void Install(IContainerBuilder builder)
    {
        new CameraCompositionInstaller().Install(builder);
        new GamePauseCompositionInstaller().Install(builder);
        new ScoreCompositionInstaller().Install(builder);
        new PlayerCompositionInstaller().Install(builder);
        new InvaderCompositionInstaller().Install(builder);
        new UFOCompositionInstaller().Install(builder);
        new ShieldCompositionInstaller().Install(builder);
    }
}
