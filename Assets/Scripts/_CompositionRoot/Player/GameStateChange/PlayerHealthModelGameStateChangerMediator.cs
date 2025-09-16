using R3;

public class PlayerHealthModelGameStateChangerMediator : Mediator
{
    private readonly PlayerHealthModel _playerHealthModel;
    private readonly GameStateChanger _gameStateChanger;

    public PlayerHealthModelGameStateChangerMediator(PlayerHealthModel playerHealthModel, 
        GameStateChanger gameStateChanger)
    {
        _playerHealthModel = playerHealthModel;
        _gameStateChanger = gameStateChanger;
    }

    public override void Initialize()
    {
        _gameStateChanger.GameRestarted
            .Subscribe(_ => _playerHealthModel.Reset())
            .AddTo(CompositeDisposable);
    }
}
