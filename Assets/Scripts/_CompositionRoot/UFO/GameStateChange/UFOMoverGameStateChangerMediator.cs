using R3;

public class UFOMoverGameStateChangerMediator : Mediator
{
    private readonly UFOMover _ufoMover;
    private readonly GameStateChanger _gameStateChanger;

    public UFOMoverGameStateChangerMediator(UFOMover ufoMover, 
        GameStateChanger gameStateChanger)
    {
        _ufoMover = ufoMover;
        _gameStateChanger = gameStateChanger;
    }

    public override void Initialize()
    {
        _gameStateChanger.GameStateChanged
            .Subscribe(_ => _ufoMover.OnGameStateChanged())
            .AddTo(CompositeDisposable);
    }
}
