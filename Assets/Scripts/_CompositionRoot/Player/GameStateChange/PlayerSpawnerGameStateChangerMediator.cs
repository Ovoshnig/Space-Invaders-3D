using R3;

public class PlayerSpawnerGameStateChangerMediator : Mediator
{
    private readonly PlayerSpawner _playerSpawner;
    private readonly GameStateChanger _gameStateChanger;

    public PlayerSpawnerGameStateChangerMediator(PlayerSpawner playerSpawner, 
        GameStateChanger gameStateChanger)
    {
        _playerSpawner = playerSpawner;
        _gameStateChanger = gameStateChanger;
    }

    public override void Initialize()
    {
        _gameStateChanger.GameStateChanged
            .Subscribe(_ => _playerSpawner.ReturnToStartPosition())
            .AddTo(CompositeDisposable);
    }
}
