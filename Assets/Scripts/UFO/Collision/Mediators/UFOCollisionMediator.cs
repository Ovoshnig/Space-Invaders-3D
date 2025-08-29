public class UFOCollisionMediator : CollisionMediator<UFOMoverView>
{
    private readonly UFOMoverView _ufoMoverView;

    public UFOCollisionMediator(CollisionReporter<UFOMoverView> collisionHub, UFOMoverView ufoMoverView)
        : base(collisionHub) => _ufoMoverView = ufoMoverView;

    public override void Initialize()
    {
        TriggerColliderView colliderView = _ufoMoverView.GetComponent<TriggerColliderView>();
        Subscribe(_ufoMoverView, colliderView);
    }
}
