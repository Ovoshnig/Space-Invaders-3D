using R3;

public class InvaderMoverGamePauserMediator : Mediator
{
    private readonly InvaderMover _invaderMover;
    private readonly GamePauser _gamePauser;

    public InvaderMoverGamePauserMediator(InvaderMover invaderMover, GamePauser gamePauser)
    {
        _invaderMover = invaderMover;
        _gamePauser = gamePauser;
    }

    public override void Initialize()
    {
        _gamePauser.IsPause
            .Subscribe(_invaderMover.SetPause)
            .AddTo(CompositeDisposable);
    }
}
