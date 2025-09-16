using R3;
using System;
using VContainer.Unity;

public class GameOver : IInitializable, IDisposable
{
    private readonly PlayerHealthModel _playerHealthModel;
    private readonly InvaderMover _invaderMover;
    private readonly GameRestarter _gameRestarter;
    private readonly ReactiveProperty<bool> _isOver = new(false);
    private readonly CompositeDisposable _compositeDisposable = new();

    public GameOver(PlayerHealthModel playerHealthModel,
        InvaderMover invaderMover,
        GameRestarter gameRestarter)
    {
        _playerHealthModel = playerHealthModel;
        _invaderMover = invaderMover;
        _gameRestarter = gameRestarter;
    }

    public ReadOnlyReactiveProperty<bool> IsOver => _isOver;

    public void Initialize()
    {
        _playerHealthModel.IsOver
            .Where(isOver => isOver)
            .Subscribe(_ => _isOver.Value = true)
            .AddTo(_compositeDisposable);

        _invaderMover.BottomReached
            .Subscribe(_ => _isOver.Value = true)
            .AddTo(_compositeDisposable);

        _gameRestarter.Restarted
            .Subscribe(_ => _isOver.Value = false)
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();
}
