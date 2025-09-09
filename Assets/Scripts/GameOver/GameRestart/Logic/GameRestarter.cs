using Cysharp.Threading.Tasks;

public class GameRestarter
{
    private readonly SceneSwitch _sceneSwitch;

    public GameRestarter(SceneSwitch sceneSwitch) => _sceneSwitch = sceneSwitch;

    public void Restart() => _sceneSwitch.LoadCurrentLevelAsync().Forget();
}
