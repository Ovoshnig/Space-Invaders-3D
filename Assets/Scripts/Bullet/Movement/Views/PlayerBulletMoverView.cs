using VContainer;

public class PlayerBulletMoverView : BulletMoverView
{
    private PlayerSettings _playerSettings;

    [Inject]
    public void Construct(FieldSettings fieldSettings, PlayerSettings playerSettings)
    {
        Construct(fieldSettings);

        _playerSettings = playerSettings;
    }

    protected override DirectionZ Direction => DirectionZ.Forward;
    protected override float Speed => _playerSettings.BulletSpeed;
}
