using R3;

public class PlayerHealthMediator : Mediator
{
    private readonly PlayerHealthModel _playerHealthModel;
    private readonly PlayerHealthView _playerHealthView;

    public PlayerHealthMediator(PlayerHealthModel playerHealthModel,
        PlayerHealthView playerHealthView)
    {
        _playerHealthModel = playerHealthModel;
        _playerHealthView = playerHealthView;
    }

    public override void Initialize()
    {
        _playerHealthModel.Health
            .Subscribe(_playerHealthView.Set)
            .AddTo(CompositeDisposable);
    }
}
