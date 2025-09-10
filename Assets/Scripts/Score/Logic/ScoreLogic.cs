using R3;
using System;
using VContainer.Unity;

public class ScoreLogic : IInitializable, IDisposable
{
    private readonly CollidedDestroyer<InvaderEntityView, PlayerBulletMoverView> _invaderDestroyer;
    private readonly UFODestroyer _ufoDestroyer;
    private readonly ScoreModel _scoreModel = new();
    private readonly ReactiveProperty<int> _lastPoints = new();
    private readonly ReactiveProperty<int> _score = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public ScoreLogic(CollidedDestroyer<InvaderEntityView, PlayerBulletMoverView> invaderDestroyer,
        UFODestroyer ufoDestroyer)
    {
        _invaderDestroyer = invaderDestroyer;
        _ufoDestroyer = ufoDestroyer;
    }

    public ReadOnlyReactiveProperty<int> LastPoints => _lastPoints;
    public ReadOnlyReactiveProperty<int> Score => _score;

    public void Initialize()
    {
        _score.Value = _scoreModel.Score;

        _lastPoints
            .Subscribe(points =>
            {
                _scoreModel.Increase(points);
                _score.Value = _scoreModel.Score;
            })
            .AddTo(_compositeDisposable);

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
        _lastPoints.OnNext(pointsView.GetPoints());
    }

    private void OnUFODestroyed(CollidedDestructionEvent<UFOMoverView, PlayerBulletMoverView> destructionEvent) =>
        _lastPoints.OnNext(_ufoDestroyer.LastPoints.CurrentValue);
}
