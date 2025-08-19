using R3;

public class InvaderSpawnerMediator : Mediator
{
    private readonly InvaderSpawner _invaderSpawner;
    private readonly InvaderSpawnerView _invaderSpawnerView;

    public InvaderSpawnerMediator(InvaderSpawner invaderSpawner, 
        InvaderSpawnerView invaderSpawnerView)
    {
        _invaderSpawner = invaderSpawner;
        _invaderSpawnerView = invaderSpawnerView;
    }

    public override void Initialize()
    {
        _invaderSpawner.PositionSelected
            .Subscribe(x => _invaderSpawnerView.SpawnInvader(x.index, x.position))
            .AddTo(CompositeDisposable);

        _invaderSpawner.StartSpawn();
    }
}
