using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameplayLifetimeScope : LifetimeScope
{
    [SerializeField] private FieldView _fieldView;
    [SerializeField] private PlayerMoverView _playerMoverView;

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterInstance(_fieldView);

        builder.RegisterComponent(_playerMoverView);
        builder.RegisterEntryPoint<PlayerInputHandler>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint<PlayerMover>(Lifetime.Singleton).AsSelf();
        builder.RegisterEntryPoint(container =>
        {
            PlayerMover playerMover = container.Resolve<PlayerMover>();
            PlayerMoverView playerMoverView = container.Resolve<PlayerMoverView>();
            return new PlayerMoverMediator(playerMover, playerMoverView);

        }, Lifetime.Singleton);
    }

    private void Start()
    {

    }
}
