using R3;

public class PlayerDestroyerAnimatorViewMediator : Mediator
{
    private readonly PlayerDestroyer _playerDestroyer;
    private readonly PlayerAnimatorView _playerAnimatorView;

    public PlayerDestroyerAnimatorViewMediator(PlayerDestroyer playerDestroyer,
        PlayerMoverView playerMoverView)
    {
        _playerDestroyer = playerDestroyer;
        _playerAnimatorView = playerMoverView.GetComponent<PlayerAnimatorView>();
    }

    public override void Initialize()
    {
        _playerDestroyer.StartDestroying
            .Subscribe(_ => _playerAnimatorView.SetExploding(true))
            .AddTo(CompositeDisposable);
        _playerDestroyer.Destroyed
            .Subscribe(_ => _playerAnimatorView.SetExploding(false))
            .AddTo(CompositeDisposable);
    }
}
