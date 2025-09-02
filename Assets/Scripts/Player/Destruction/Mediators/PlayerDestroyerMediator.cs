using R3;

public class PlayerDestroyerMediator : CollidedDestroyerMediator<PlayerMoverView, InvaderBulletMoverView>
{
    private readonly PlayerDestroyer _playerDestroyer;

    public PlayerDestroyerMediator(PlayerDestroyer playerDestroyer) : base(playerDestroyer) => 
        _playerDestroyer = playerDestroyer;

    public override void Initialize()
    {
        _playerDestroyer.StartDestroying
            .Subscribe(OnStartDestroying)
            .AddTo(CompositeDisposable);

        base.Initialize();
    }

    protected override CollidedDestroyerView<InvaderBulletMoverView> GetDestroyerView(PlayerMoverView playerMoverView) =>
        playerMoverView.GetComponent<PlayerDestroyerView>();

    private void OnStartDestroying(CollidedDestructionEvent<PlayerMoverView, InvaderBulletMoverView> collidedDestructionEvent)
    {
        PlayerDestroyerView playerDestroyerView
            = GetDestroyerView(collidedDestructionEvent.CollidedView) as PlayerDestroyerView;
        playerDestroyerView.StartDestroy(collidedDestructionEvent.OtherView);
    }
}
