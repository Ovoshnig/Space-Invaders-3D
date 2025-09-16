using R3;

public class ShieldTileGameStateChangerMediator : Mediator
{
    private readonly ShieldTileView[] _shieldTileViews;
    private readonly GameStateChanger _gameStateChanger;

    public ShieldTileGameStateChangerMediator(ShieldTileView[] shieldTileViews, 
        GameStateChanger gameStateChanger)
    {
        _shieldTileViews = shieldTileViews;
        _gameStateChanger = gameStateChanger;
    }

    public override void Initialize()
    {
        _gameStateChanger.GameStateChanged
            .Subscribe(_ => OnInvadersDestroyed())
            .AddTo(CompositeDisposable);
    }

    private void OnInvadersDestroyed()
    {
        foreach (var tile in _shieldTileViews)
            tile.Restore();
    }
}
