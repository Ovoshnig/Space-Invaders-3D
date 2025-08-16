using R3;

public class PlayerInputHandler : InputHandler<InputActions.PlayerActions>
{
    public PlayerInputHandler(InputActions inputActions)
        : base(inputActions.Player) { }

    public ReadOnlyReactiveProperty<float> WalkInput { get; private set; }

    public override void Initialize()
    {
        base.Initialize();

        WalkInput = BindValue<float>(a => a.Walk);
    }

    protected override void EnableActions() => Actions.Enable();

    protected override void DisableActions() => Actions.Disable();
}
