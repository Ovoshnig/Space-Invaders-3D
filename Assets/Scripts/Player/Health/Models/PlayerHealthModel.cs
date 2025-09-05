public class PlayerHealthModel
{
    private int _health;

    public PlayerHealthModel(PlayerSettings playerSettings) =>
        _health = playerSettings.InitialHealth;

    public int Health => _health;

    public void Decrement()
    {
        if (_health > 0)
            _health--;
    }
}
