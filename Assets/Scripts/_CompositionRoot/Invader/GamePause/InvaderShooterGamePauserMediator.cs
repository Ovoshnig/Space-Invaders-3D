using R3;

public class InvaderShooterGamePauserMediator : Mediator
{
    private readonly InvaderShooter _invaderShooter;
    private readonly GamePauser _gamePauser;

    public InvaderShooterGamePauserMediator(InvaderShooter invaderShooter, GamePauser gamePauser)
    {
        _invaderShooter = invaderShooter;
        _gamePauser = gamePauser;
    }

    public override void Initialize()
    {
        _gamePauser.IsPause
            .Subscribe(_invaderShooter.SetPause)
            .AddTo(CompositeDisposable);
    }
}
