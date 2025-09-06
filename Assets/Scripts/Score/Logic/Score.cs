using R3;
using System;
using VContainer.Unity;

public class Score : IInitializable, IDisposable
{
    private readonly CollidedDestroyer<InvaderEntityView, PlayerBulletMoverView> _invaderDestroyer;
    private readonly CollidedDestroyer<UFOMoverView, PlayerBulletMoverView> _ufoDestroyer;
    private readonly InvaderPointsSettings _invaderPointsSettings;
    private readonly ScoreModel _scoreModel = new();
    private readonly ReactiveProperty<int> _score = new();
    private readonly Random _random = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public Score(CollidedDestroyer<InvaderEntityView, PlayerBulletMoverView> invaderDestroyer,
        CollidedDestroyer<UFOMoverView, PlayerBulletMoverView> ufoDestroyer,
        InvaderPointsSettings invaderPointsSettings)
    {
        _invaderDestroyer = invaderDestroyer;
        _ufoDestroyer = ufoDestroyer;
        _invaderPointsSettings = invaderPointsSettings;
    }

    public ReadOnlyReactiveProperty<int> Changed => _score;

    public void Initialize()
    {
        _score.Value = _scoreModel.Score;

        _invaderDestroyer.Destroyed
            .Subscribe(OnInvaderDestroyed)
            .AddTo(_compositeDisposable);
        _ufoDestroyer.Destroyed
            .Subscribe(OnUFODestroyed)
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();

    private void OnInvaderDestroyed(CollidedDestructionEvent<InvaderEntityView, PlayerBulletMoverView> destructionEvent)
    {
        InvaderPointsView pointsView = destructionEvent.CollidedView.Get<InvaderPointsView>();
        int points = pointsView.GetPoints();
        _scoreModel.Increase(points);
        _score.Value = _scoreModel.Score;
    }

    private void OnUFODestroyed(CollidedDestructionEvent<UFOMoverView, PlayerBulletMoverView> destructionEvent)
    {
        int randomIndex = _random.Next(_invaderPointsSettings.UFOPointsVariants.Length);
        int randomPoints = _invaderPointsSettings.UFOPointsVariants[randomIndex];
        _scoreModel.Increase(randomPoints);
        _score.Value = _scoreModel.Score;
    }
}
