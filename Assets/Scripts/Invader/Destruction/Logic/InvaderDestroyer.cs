public class InvaderDestroyer : CollidedDestroyer<InvaderEntityView, PlayerBulletMoverView>
{
    public InvaderDestroyer(CollisionReporter<InvaderEntityView> collider)
        : base(collider) { }
}
