using R3;

public class GamePauserPlayerDestroyerMediator : Mediator
{
    private readonly GamePauser _gamePauser;
    private readonly PlayerDestroyer _playerDestroyer;

    public GamePauserPlayerDestroyerMediator(GamePauser gamePauser, 
        PlayerDestroyer playerDestroyer)
    {
        _gamePauser = gamePauser;
        _playerDestroyer = playerDestroyer;
    }

    public override void Initialize()
    {
        _playerDestroyer.StartDestroying
            .Subscribe(_ => _gamePauser.Pause())
            .AddTo(CompositeDisposable);
        _playerDestroyer.Destroyed
            .Subscribe(_ => _gamePauser.UnPause())
            .AddTo(CompositeDisposable);
    }
}
