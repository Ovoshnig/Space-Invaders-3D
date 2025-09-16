using R3;

public class PlayerBulletMoverGamePauserMediator : Mediator
{
    private readonly PlayerBulletMoverView _playerBulletMoverView;
    private readonly GamePauser _gamePauser;

    public PlayerBulletMoverGamePauserMediator(PlayerBulletMoverView playerBulletMoverView, GamePauser gamePauser)
    {
        _playerBulletMoverView = playerBulletMoverView;
        _gamePauser = gamePauser;
    }

    public override void Initialize()
    {
        _gamePauser.IsPause
            .Where(isPause => isPause)
            .Subscribe(_ => _playerBulletMoverView.gameObject.SetActive(false))
            .AddTo(CompositeDisposable);
    }
}
