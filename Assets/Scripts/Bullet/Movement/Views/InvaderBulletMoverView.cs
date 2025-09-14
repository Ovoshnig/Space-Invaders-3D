public class InvaderBulletMoverView : BulletMoverView
{
    protected override DirectionZ Direction => DirectionZ.Backward;
    protected override float Speed => GameSettings.InvaderSettings.InvaderShootingSettings.BulletSpeed;
}
