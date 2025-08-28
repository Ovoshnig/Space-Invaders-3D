public class PlayerShooterData
{
    private int _shotsCount = 0;

    public int ShotsCount => _shotsCount;

    public void Increment() => _shotsCount++;
}
