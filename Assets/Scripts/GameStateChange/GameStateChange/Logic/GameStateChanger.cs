using R3;
using System;
using VContainer.Unity;

public class GameStateChanger : IInitializable, IDisposable
{
    private readonly InvaderRegistry _invaderRegistry;
    private readonly GameRestarter _gameRestarter;
    private readonly Subject<Unit> _invadersDestroyed = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public GameStateChanger(InvaderRegistry invaderRegistry, GameRestarter gameRestarter)
    {
        _invaderRegistry = invaderRegistry;
        _gameRestarter = gameRestarter;

        GameRestarted = _gameRestarter.Restarted;
        GameStateChanged = Observable.Merge(InvadersDestroyed, GameRestarted);
    }

    public Observable<Unit> InvadersDestroyed => _invadersDestroyed;
    public Observable<Unit> GameRestarted { get; private set; }
    public Observable<Unit> GameStateChanged { get; private set; }

    public void Initialize()
    {
        _invaderRegistry.Any
            .Where(any => !any)
            .Skip(1)
            .Subscribe(_ => _invadersDestroyed.OnNext(Unit.Default))
            .AddTo(_compositeDisposable);
    }

    public void Dispose() => _compositeDisposable.Dispose();
}
