using R3;

public class UFOMoverMediator : Mediator
{
    private readonly UFOMover _UFOMover;
    private readonly UFOMoverView _UFOMoverView;

    public UFOMoverMediator(UFOMover uFOMover, UFOMoverView uFOMoverView)
    {
        _UFOMover = uFOMover;
        _UFOMoverView = uFOMoverView;
    }

    public override void Initialize()
    {
        _UFOMover.Started
            .Subscribe(_UFOMoverView.StartMovement)
            .AddTo(CompositeDisposable);
        _UFOMover.Ended
            .Subscribe(_ => _UFOMoverView.StopMovement())
            .AddTo(CompositeDisposable);
        _UFOMover.Moved
            .Subscribe(_UFOMoverView.Move)
            .AddTo(CompositeDisposable);
    }
}
