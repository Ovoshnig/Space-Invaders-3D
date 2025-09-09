using R3;
using System;
using VContainer.Unity;

public class GameOver : IInitializable, IDisposable
{
    private readonly PlayerHealthLogic _playerHealthLogic;
    private readonly InvaderMover _invaderMover;
    private readonly Subject<Unit> _over = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public GameOver(PlayerHealthLogic playerHealthLogic, InvaderMover invaderMover)
    {
        _playerHealthLogic = playerHealthLogic;
        _invaderMover = invaderMover;
    }

    public Observable<Unit> Over => _over;

    public void Initialize()
    {
        _playerHealthLogic.HealthOver
            .Subscribe(_ => _over.OnNext(Unit.Default))
            .AddTo(_compositeDisposable);

        _invaderMover.BottomReached
            .Subscribe(_ => _over.OnNext(Unit.Default))
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();
}
