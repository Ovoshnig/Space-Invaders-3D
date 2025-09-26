using R3;

public class InvaderRegistryGameStateChangerMediator : Mediator
{
    private readonly InvaderRegistry _invaderRegistry;
    private readonly GameStateChanger _gameStateChanger;

    public InvaderRegistryGameStateChangerMediator(InvaderRegistry IivaderRegistry,
        GameStateChanger gameStateChanger)
    {
        _invaderRegistry = IivaderRegistry;
        _gameStateChanger = gameStateChanger;
    }

    public override void Initialize()
    {
        _gameStateChanger.GameRestarted
            .Subscribe(_ => OnGameRestarted())
            .AddTo(CompositeDisposable);
    }

    private void OnGameRestarted()
    {
        int invadersCount = _invaderRegistry.Invaders.Count;

        for (int i = 0; i < invadersCount; i++)
        {
            InvaderEntityView invader = _invaderRegistry.Invaders[0];
            invader.gameObject.SetActive(false);
        }
    }
}
