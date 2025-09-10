using R3;

public class UFODestroyerMediator : CollidedDestroyerMediator<UFOMoverView, PlayerBulletMoverView>
{
    private readonly UFODestroyer _ufoDestroyer;
    private readonly UFODestroyerView _ufoDestroyerView;

    public UFODestroyerMediator(UFODestroyer ufoDestroyer, UFODestroyerView ufoDestroyerView) 
        : base(ufoDestroyer)
    {
        _ufoDestroyer = ufoDestroyer;
        _ufoDestroyerView = ufoDestroyerView;
    }

    public override void Initialize()
    {
        base.Initialize();

        _ufoDestroyer.LastPoints
            .Subscribe(_ufoDestroyerView.SetLastPoints)
            .AddTo(CompositeDisposable);
    }

    protected override CollidedDestroyerView<PlayerBulletMoverView> GetDestroyerView(UFOMoverView ufoMoverView) =>
        _ufoDestroyerView;
}
