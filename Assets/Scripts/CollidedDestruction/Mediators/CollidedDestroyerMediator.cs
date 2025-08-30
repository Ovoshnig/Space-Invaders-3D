using R3;
using UnityEngine;

public abstract class CollidedDestroyerMediator<TCollidedView, TOtherView> : Mediator
    where TCollidedView : MonoBehaviour
    where TOtherView : MonoBehaviour
{
    private readonly CollidedDestroyer<TCollidedView, TOtherView> _invaderDestroyer;

    public CollidedDestroyerMediator(CollidedDestroyer<TCollidedView, TOtherView> invaderDestroyer) =>
        _invaderDestroyer = invaderDestroyer;

    public override void Initialize()
    {
        _invaderDestroyer.Destroyed
            .Subscribe(OnDestroyed)
            .AddTo(CompositeDisposable);
    }

    protected abstract CollidedDestroyerView<TOtherView> GetDestroyerView(TCollidedView collidedView);

    private void OnDestroyed(CollidedDestructionEvent<TCollidedView, TOtherView> collidedDestructionEvent)
    {
        CollidedDestroyerView<TOtherView> destroyerView = GetDestroyerView(collidedDestructionEvent.CollidedView);
        destroyerView.Destroy(collidedDestructionEvent.OtherView);
    }
}
