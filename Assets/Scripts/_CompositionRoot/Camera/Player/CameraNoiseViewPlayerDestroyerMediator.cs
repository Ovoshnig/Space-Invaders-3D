using R3;

public class CameraNoiseViewPlayerDestroyerMediator : Mediator
{
    private readonly CameraNoiseView _cameraNoiseView;
    private readonly PlayerDestroyer _playerDestroyer;

    public CameraNoiseViewPlayerDestroyerMediator(CameraNoiseView cameraNoiseView,
        PlayerDestroyer playerDestroyer)
    {
        _cameraNoiseView = cameraNoiseView;
        _playerDestroyer = playerDestroyer;
    }

    public override void Initialize()
    {
        _playerDestroyer.StartDestroying
            .Subscribe(_ => _cameraNoiseView.SetNoise())
            .AddTo(CompositeDisposable);
        _playerDestroyer.Destroyed
            .Subscribe(_ => _cameraNoiseView.SetNormal())
            .AddTo(CompositeDisposable);
    }
}
