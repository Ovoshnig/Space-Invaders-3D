public class InvaderDestroyer : CollidedDestroyer<InvaderEntityView, PlayerBulletMoverView>
{
    public InvaderDestroyer(InvaderCollisionReporter collisionReporter)
        : base(collisionReporter) { }
}
