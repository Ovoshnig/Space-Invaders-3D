public class UFODestroyerMediator : CollidedDestroyerMediator<UFOMoverView, PlayerBulletMoverView>
{
    public UFODestroyerMediator(CollidedDestroyer<UFOMoverView, PlayerBulletMoverView> invaderDestroyer)
        : base(invaderDestroyer) { }

    protected override CollidedDestroyerView<PlayerBulletMoverView> GetDestroyerView(UFOMoverView ufoMoverView) =>
        ufoMoverView.GetComponent<UFODestroyerView>();
}
