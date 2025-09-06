using R3;

public class PlayerMoverMediator : Mediator
{
    private readonly PlayerMover _playerMover;
    private readonly PlayerMoverView _playerMoverView;

    public PlayerMoverMediator(PlayerMover playerMover, PlayerMoverView playerMoverView)
    {
        _playerMover = playerMover;
        _playerMoverView = playerMoverView;
    }

    public override void Initialize()
    {
        _playerMover.FrameMovement
            .Subscribe(_playerMoverView.Move)
            .AddTo(CompositeDisposable);
    }
}
