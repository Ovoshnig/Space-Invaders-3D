using R3;

public class GamePauserInvaderSpawnerMediator : Mediator
{
    private readonly GamePauser _gamePauser;
    private readonly InvaderSpawner _invaderSpawner;

    public GamePauserInvaderSpawnerMediator(GamePauser gamePauser, 
        InvaderSpawner invaderSpawner)
    {
        _gamePauser = gamePauser;
        _invaderSpawner = invaderSpawner;
    }

    public override void Initialize()
    {
        _invaderSpawner.Started
            .Subscribe(_ => _gamePauser.Pause())
            .AddTo(CompositeDisposable);
        _invaderSpawner.Ended
            .Subscribe(_ => _gamePauser.UnPause())
            .AddTo(CompositeDisposable);
    }
}
