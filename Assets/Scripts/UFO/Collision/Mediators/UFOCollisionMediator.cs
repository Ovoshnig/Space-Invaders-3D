public class UFOCollisionMediator : CollisionMediator<UFOMoverView>
{
    private readonly UFOMoverView _ufoMoverView;

    public UFOCollisionMediator(UFOCollisionReporter collisionReporter,
        UFOMoverView ufoMoverView)
        : base(collisionReporter) => _ufoMoverView = ufoMoverView;

    public override void Initialize()
    {
        TriggerColliderView colliderView = _ufoMoverView.GetComponent<TriggerColliderView>();
        Subscribe(_ufoMoverView, colliderView);
    }
}
