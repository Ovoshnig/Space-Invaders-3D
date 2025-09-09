using R3;

public class GamePauserGameOverMediator : Mediator
{
    private readonly GamePauser _gamePauser;
    private readonly GameOver _gameOver;

    public GamePauserGameOverMediator(GamePauser gamePauser,
        GameOver gameOver)
    {
        _gamePauser = gamePauser;
        _gameOver = gameOver;
    }

    public override void Initialize()
    {
        _gameOver.Over
            .Subscribe(_ => _gamePauser.Pause())
            .AddTo(CompositeDisposable);
    }
}
