using R3;
using System;
using VContainer.Unity;

public class ScoreLogic : IInitializable, IDisposable
{
    private readonly InvaderDestroyer _invaderDestroyer;
    private readonly UFODestroyer _ufoDestroyer;
    private readonly ScoreModel _scoreModel;
    private readonly ReactiveProperty<int> _lastPoints = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public ScoreLogic(InvaderDestroyer invaderDestroyer,
        UFODestroyer ufoDestroyer,
        ScoreModel scoreModel)
    {
        _invaderDestroyer = invaderDestroyer;
        _ufoDestroyer = ufoDestroyer;
        _scoreModel = scoreModel;
    }

    public ReadOnlyReactiveProperty<int> LastPoints => _lastPoints;

    public void Initialize()
    {
        _lastPoints
            .Subscribe(points => _scoreModel.Increase(points))
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
        InvaderPointsView pointsView = destructionEvent.CollidedView.PointsView;
        _lastPoints.OnNext(pointsView.GetPoints());
    }

    private void OnUFODestroyed(CollidedDestructionEvent<UFOMoverView, PlayerBulletMoverView> destructionEvent) =>
        _lastPoints.OnNext(_ufoDestroyer.LastPoints.CurrentValue);
}
