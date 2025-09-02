public class PlayerDestroyerView : CollidedDestroyerView<InvaderBulletMoverView>
{
    public void StartDestroy(InvaderBulletMoverView invaderBulletView) => 
        invaderBulletView.gameObject.SetActive(false);

    public override void Destroy(InvaderBulletMoverView invaderBulletView)
    {
    }
}
