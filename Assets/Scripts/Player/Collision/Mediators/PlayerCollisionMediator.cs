public class PlayerCollisionMediator : CollisionMediator<PlayerMoverView>
{
    private readonly PlayerMoverView _playerMoverView;

    public PlayerCollisionMediator(PlayerCollisionReporter playerCollisionReporter,
        PlayerMoverView playerMoverView)
        : base(playerCollisionReporter) => _playerMoverView = playerMoverView;

    public override void Initialize()
    {
        TriggerColliderView colliderView = _playerMoverView.GetComponent<TriggerColliderView>();
        Subscribe(_playerMoverView, colliderView);
    }
}
