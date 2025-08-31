public class PlayerCollisionMediator : CollisionMediator<PlayerMoverView>
{
    private readonly PlayerMoverView _playerMoverView;

    public PlayerCollisionMediator(CollisionReporter<PlayerMoverView> collisionReporter, 
        PlayerMoverView playerMoverView) 
        : base(collisionReporter) => _playerMoverView = playerMoverView;

    public override void Initialize()
    {
        TriggerColliderView colliderView = _playerMoverView.GetComponent<TriggerColliderView>();
        Subscribe(_playerMoverView, colliderView);
    }
}
