using R3;

public class InvaderBulletPoolGamePauserMediator : Mediator
{
    private readonly InvaderBulletPool _invaderBulletPool;
    private readonly GamePauser _gamePauser;

    public InvaderBulletPoolGamePauserMediator(InvaderBulletPool invaderBulletPool,
        GamePauser gamePauser)
    {
        _invaderBulletPool = invaderBulletPool;
        _gamePauser = gamePauser;
    }

    public override void Initialize()
    {
        _gamePauser.IsPause
            .Where(isPause => isPause)
            .Subscribe(_ => _invaderBulletPool.ReleaseAllActive())
            .AddTo(CompositeDisposable);
    }
}
