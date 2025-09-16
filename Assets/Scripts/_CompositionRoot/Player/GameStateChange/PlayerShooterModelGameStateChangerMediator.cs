using R3;

public class PlayerShooterModelGameStateChangerMediator : Mediator
{
    private readonly PlayerShooterModel _playerShooterModel;
    private readonly GameStateChanger _gameStateChanger;

    public PlayerShooterModelGameStateChangerMediator(PlayerShooterModel playerShooterModel, 
        GameStateChanger gameStateChanger)
    {
        _playerShooterModel = playerShooterModel;
        _gameStateChanger = gameStateChanger;
    }

    public override void Initialize()
    {
        _gameStateChanger.GameStateChanged
            .Subscribe(_ => _playerShooterModel.Reset())
            .AddTo(CompositeDisposable);
    }
}
