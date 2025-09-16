using R3;
using VContainer.Unity;

public class PlayerHealthLogic : IInitializable
{
    private readonly PlayerHealthModel _playerHealthModel;
    private readonly PlayerDestroyer _playerDestroyer;
    private readonly CompositeDisposable _compositeDisposable = new();

    public PlayerHealthLogic(PlayerHealthModel playerHealthModel,
        PlayerDestroyer playerDestroyer)
    {
        _playerHealthModel = playerHealthModel;
        _playerDestroyer = playerDestroyer;
    }

    public void Initialize()
    {
        _playerDestroyer.Destroyed
            .Subscribe(_ => _playerHealthModel.Decrement())
            .AddTo(_compositeDisposable);
    }
}
