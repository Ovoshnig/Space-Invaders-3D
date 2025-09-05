using R3;
using VContainer.Unity;

public class PlayerHealthLogic : IInitializable
{
    private readonly PlayerHealthModel _playerHealthModel;
    private readonly PlayerDestroyer _playerDestroyer;
    private readonly ReactiveProperty<int> _health = new();
    private readonly Subject<Unit> _healthOver = new();
    private readonly CompositeDisposable _compositeDisposable = new();

    public PlayerHealthLogic(PlayerHealthModel playerHealthModel,
        PlayerDestroyer playerDestroyer)
    {
        _playerHealthModel = playerHealthModel;
        _playerDestroyer = playerDestroyer;
    }

    public ReadOnlyReactiveProperty<int> Health => _health;
    public Observable<Unit> HealthOver => _healthOver;

    public void Initialize()
    {
        _health.Value = _playerHealthModel.Health;

        _playerDestroyer.Destroyed
            .Subscribe(_ => OnDestroyed())
            .AddTo(_compositeDisposable);
    }

    private void OnDestroyed()
    {
        _playerHealthModel.Decrement();
        _health.Value = _playerHealthModel.Health;

        if (_playerHealthModel.Health == 0)
            _healthOver.OnNext(Unit.Default);
    }
}
