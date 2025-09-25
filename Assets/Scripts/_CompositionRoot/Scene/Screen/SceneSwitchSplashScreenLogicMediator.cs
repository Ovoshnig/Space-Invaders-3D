using Cysharp.Threading.Tasks;
using R3;

public class SceneSwitchSplashScreenLogicMediator : Mediator
{
    private readonly SceneSwitch _sceneSwitch;
    private readonly SplashScreenLogic _splashScreenLogic;

    public SceneSwitchSplashScreenLogicMediator(SceneSwitch sceneSwitch, 
        SplashScreenLogic splashScreenLogic)
    {
        _sceneSwitch = sceneSwitch;
        _splashScreenLogic = splashScreenLogic;
    }

    public override void Initialize()
    {
        _splashScreenLogic.IsPlaying
            .Where(isPlaying => !isPlaying)
            .Subscribe(_ => _sceneSwitch.LoadLevelAsync(1).Forget())
            .AddTo(CompositeDisposable);
    }
}
