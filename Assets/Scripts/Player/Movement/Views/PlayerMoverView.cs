using UnityEngine;
using VContainer;

public class PlayerMoverView : MonoBehaviour
{
    private FieldView _fieldView;
    private MeshRenderer _meshRenderer;

    [Inject]
    public void Construct(FieldView fieldView) => _fieldView = fieldView;

    private MeshRenderer MeshRenderer
    {
        get
        {
            if (_meshRenderer == null)
                _meshRenderer = GetComponentInChildren<MeshRenderer>();

            return _meshRenderer;
        }
        set => _meshRenderer = value;
    }

    public void Move(Vector3 motion)
    {
        var innerBounds = MeshRenderer.bounds;
        var outerBounds = _fieldView.Bounds;

        if (innerBounds.min.x + motion.x >= outerBounds.min.x && 
            innerBounds.max.x + motion.x <= outerBounds.max.x)
            transform.Translate(motion, Space.World);
    }
}
