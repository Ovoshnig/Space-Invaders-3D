using UnityEngine;
using VContainer;

public class PlayerMoverView : MonoBehaviour
{
    private FieldView _fieldView;
    private BoxCollider _boxCollider = null;

    [Inject]
    public void Construct(FieldView fieldView) => _fieldView = fieldView;

    private BoxCollider BoxCollider
    {
        get
        {
            if (_boxCollider == null)
                _boxCollider = GetComponent<BoxCollider>();

            return _boxCollider;
        }
    }

    public void Move(Vector3 movement)
    {
        float halfWidth = BoxCollider.bounds.extents.x;
        float minX = _fieldView.Bounds.min.x + halfWidth;
        float maxX = _fieldView.Bounds.max.x - halfWidth;

        Vector3 nextPosition = transform.position + movement;
        nextPosition.x = Mathf.Clamp(nextPosition.x, minX, maxX);
        transform.position = nextPosition;
    }
}
