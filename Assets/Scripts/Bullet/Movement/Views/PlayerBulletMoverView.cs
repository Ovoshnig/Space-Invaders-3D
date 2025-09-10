using VContainer;

public class PlayerBulletMoverView : BulletMoverView
{
    private PlayerSettings _playerSettings;

    [Inject]
    public void Construct(FieldView fieldView, PlayerSettings playerSettings)
    {
        Construct(fieldView);

        _playerSettings = playerSettings;
    }

    protected override DirectionZ Direction => DirectionZ.Forward;
    protected override float Speed => _playerSettings.BulletSpeed;
}
