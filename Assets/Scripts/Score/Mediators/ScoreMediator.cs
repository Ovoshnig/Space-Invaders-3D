using R3;

public class ScoreMediator : Mediator
{
    private readonly Score _score;
    private readonly ScoreView _scoreView;

    public ScoreMediator(Score score, ScoreView scoreView)
    {
        _score = score;
        _scoreView = scoreView;
    }

    public override void Initialize()
    {
        _score.Changed
            .Subscribe(_scoreView.SetScore)
            .AddTo(CompositeDisposable);
    }
}
