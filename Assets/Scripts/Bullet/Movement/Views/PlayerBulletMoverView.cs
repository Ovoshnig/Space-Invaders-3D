public class PlayerBulletMoverView : BulletMoverView
{
    protected override DirectionZ Direction => DirectionZ.Forward;
    protected override float Speed => GameSettings.PlayerSettings.BulletSpeed;
}
