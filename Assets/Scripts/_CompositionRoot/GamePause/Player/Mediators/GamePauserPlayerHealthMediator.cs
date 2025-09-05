using R3;

public class GamePauserPlayerHealthMediator : Mediator
{
    private readonly GamePauser _gamePauser;
    private readonly PlayerHealthLogic _playerHealthLogic;

    public GamePauserPlayerHealthMediator(GamePauser gamePauser,
        PlayerHealthLogic playerHealthLogic)
    {
        _gamePauser = gamePauser;
        _playerHealthLogic = playerHealthLogic;
    }

    public override void Initialize()
    {
        _playerHealthLogic.HealthOver
            .Subscribe(_ => _gamePauser.Pause())
            .AddTo(CompositeDisposable);
    }
}
