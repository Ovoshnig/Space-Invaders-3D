using R3;

public class UFOMoverGamePauserMediator : Mediator
{
    private readonly UFOMover _ufoMover;
    private readonly GamePauser _gamePauser;

    public UFOMoverGamePauserMediator(UFOMover ufoMover, GamePauser gamePauser)
    {
        _ufoMover = ufoMover;
        _gamePauser = gamePauser;
    }

    public override void Initialize()
    {
        _gamePauser.IsPause
            .Subscribe(_ufoMover.SetPause)
            .AddTo(CompositeDisposable);
    }
}
