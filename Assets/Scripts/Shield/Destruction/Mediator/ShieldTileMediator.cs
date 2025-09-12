using R3;

public class ShieldTileMediator : Mediator
{
    private readonly ShieldTileView[] _shieldTileViews;
    private readonly InvaderRegistry _invaderRegistry;

    public ShieldTileMediator(ShieldTileView[] shieldTileViews, 
        InvaderRegistry invaderRegistry)
    {
        _shieldTileViews = shieldTileViews;
        _invaderRegistry = invaderRegistry;
    }

    public override void Initialize()
    {
        _invaderRegistry.Any
            .Where(any => !any)
            .Subscribe(_ => OnInvadersEmpty())
            .AddTo(CompositeDisposable);
    }

    private void OnInvadersEmpty()
    {
        foreach (var tile in _shieldTileViews)
            tile.Restore();
    }
}
