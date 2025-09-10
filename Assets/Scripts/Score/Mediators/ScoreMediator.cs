using R3;

public class ScoreMediator : Mediator
{
    private readonly ScoreLogic _scoreLogic;
    private readonly ScoreView _scoreView;

    public ScoreMediator(ScoreLogic scoreLogic, ScoreView scoreView)
    {
        _scoreLogic = scoreLogic;
        _scoreView = scoreView;
    }

    public override void Initialize()
    {
        _scoreLogic.Score
            .Subscribe(_scoreView.SetScore)
            .AddTo(CompositeDisposable);
    }
}
