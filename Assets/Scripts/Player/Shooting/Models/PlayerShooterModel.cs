using R3;

public class PlayerShooterModel
{
    private readonly ReactiveProperty<int> _shotCount = new(0);

    public ReadOnlyReactiveProperty<int> ShotCount => _shotCount;

    public void Increment() => _shotCount.Value++;
}
