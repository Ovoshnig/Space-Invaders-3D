using R3;

public class PlayerStateAnimatorViewMediator : Mediator
{
    private readonly PlayerInputHandler _playerInputHandler;
    private readonly PlayerAnimatorView _playerAnimatorView;

    public PlayerStateAnimatorViewMediator(PlayerInputHandler playerInputHandler,
        PlayerAnimatorView playerAnimatorView)
    {
        _playerInputHandler = playerInputHandler;
        _playerAnimatorView = playerAnimatorView;
    }

    public override void Initialize()
    {
        _playerInputHandler.WalkInput
            .Select(walkInput => walkInput != 0f)
            .Subscribe(isWalking => _playerAnimatorView.SetWalking(isWalking))
            .AddTo(CompositeDisposable);
    }
}
