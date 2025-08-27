using R3;

public class InvaderDestroyerMediator : Mediator
{
    private readonly InvaderDestroyer _invaderDestroyer;

    public InvaderDestroyerMediator(InvaderDestroyer invaderDestroyer) =>
        _invaderDestroyer = invaderDestroyer;

    public override void Initialize()
    {
        _invaderDestroyer.Destroyed
            .Subscribe(invader => invader.Get<InvaderDestroyerView>().Destroy())
            .AddTo(CompositeDisposable);
    }
}
