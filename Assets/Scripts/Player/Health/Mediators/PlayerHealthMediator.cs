using R3;

public class PlayerHealthMediator : Mediator
{
    private readonly PlayerHealthLogic _playerHealthLogic;
    private readonly PlayerHealthView _playerHealthView;

    public PlayerHealthMediator(PlayerHealthLogic playerHealthLogic,
        PlayerHealthView playerHealthView)
    {
        _playerHealthLogic = playerHealthLogic;
        _playerHealthView = playerHealthView;
    }

    public override void Initialize()
    {
        _playerHealthLogic.Health
            .Subscribe(_playerHealthView.Set)
            .AddTo(CompositeDisposable);
    }
}
