using VContainer;

public class InvaderBulletMoverView : BulletMoverView
{
    private InvaderShootingSettings _invaderShootingSettings;

    [Inject]
    public void Construct(FieldView fieldView, InvaderShootingSettings invaderShootingSettings)
    {
        Construct(fieldView);

        _invaderShootingSettings = invaderShootingSettings;
    }

    protected override DirectionZ Direction => DirectionZ.Backward;
    protected override float Speed => _invaderShootingSettings.BulletSpeed;
}
