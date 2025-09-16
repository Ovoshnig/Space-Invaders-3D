using R3;

public class PlayerHealthModel
{
    private readonly PlayerSettings _playerSettings;
    private readonly ReactiveProperty<int> _health = new();

    public PlayerHealthModel(PlayerSettings playerSettings)
    {
        _playerSettings = playerSettings;

        Reset();
        IsOver = Health.Select(health => health == 0).ToReadOnlyReactiveProperty();
    }

    public ReadOnlyReactiveProperty<int> Health => _health;
    public ReadOnlyReactiveProperty<bool> IsOver { get; }

    public void Decrement()
    {
        if (_health.Value > 0)
            _health.Value--;
    }

    public void Reset() => _health.Value = _playerSettings.InitialHealth;
}
