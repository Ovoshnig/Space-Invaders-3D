using R3;

public class ScoreModelGameStateChangerMediator : Mediator
{
    private readonly ScoreModel _scoreModel;
    private readonly GameStateChanger _gameStateChanger;

    public ScoreModelGameStateChangerMediator(ScoreModel scoreModel, 
        GameStateChanger gameStateChanger)
    {
        _scoreModel = scoreModel;
        _gameStateChanger = gameStateChanger;
    }

    public override void Initialize()
    {
        _gameStateChanger.GameRestarted
            .Subscribe(_ => _scoreModel.Reset())
            .AddTo(CompositeDisposable);
    }
}
