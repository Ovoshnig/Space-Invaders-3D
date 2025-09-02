using UnityEngine;
using VContainer;

public class PlayerMoverView : MonoBehaviour
{
    private FieldView _fieldView;
    private BoxCollider _boxCollider;

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
        set => _boxCollider = value;
    }

    public void Move(Vector3 motion)
    {
        Bounds innerBounds = BoxCollider.bounds;
        Bounds outerBounds = _fieldView.Bounds;

        if (innerBounds.min.x + motion.x >= outerBounds.min.x && 
            innerBounds.max.x + motion.x <= outerBounds.max.x)
            transform.Translate(motion, Space.World);
    }
}
