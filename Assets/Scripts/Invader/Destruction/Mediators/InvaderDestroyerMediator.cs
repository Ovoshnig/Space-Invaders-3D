public class InvaderDestroyerMediator : CollidedDestroyerMediator<InvaderEntityView, PlayerBulletMoverView>
{
    public InvaderDestroyerMediator(CollidedDestroyer<InvaderEntityView, PlayerBulletMoverView> invaderDestroyer)
        : base(invaderDestroyer) { }

    protected override CollidedDestroyerView<PlayerBulletMoverView> GetDestroyerView(InvaderEntityView invaderEntityView) =>
        invaderEntityView.Get<InvaderDestroyerView>();
}
