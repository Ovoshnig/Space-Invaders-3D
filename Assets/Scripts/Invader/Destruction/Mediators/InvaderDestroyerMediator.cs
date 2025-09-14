public class InvaderDestroyerMediator : CollidedDestroyerMediator<InvaderEntityView, PlayerBulletMoverView>
{
    public InvaderDestroyerMediator(InvaderDestroyer invaderDestroyer)
        : base(invaderDestroyer) { }

    protected override CollidedDestroyerView<PlayerBulletMoverView> GetDestroyerView(InvaderEntityView invaderEntityView) =>
        invaderEntityView.DestroyerView;
}
