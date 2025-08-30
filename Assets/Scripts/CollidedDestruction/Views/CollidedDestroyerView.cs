using UnityEngine;

public abstract class CollidedDestroyerView<TOtherView> : MonoBehaviour
    where TOtherView : MonoBehaviour
{
    public abstract void Destroy(TOtherView other);
}
