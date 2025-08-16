using UnityEngine;

public class FieldView : MonoBehaviour
{
    private MeshRenderer _meshRenderer;

    public Bounds Bounds => MeshRenderer.bounds;

    private MeshRenderer MeshRenderer
    {
        get
        {
            if (_meshRenderer == null)
                _meshRenderer = GetComponent<MeshRenderer>();

            return _meshRenderer;
        }
        set => _meshRenderer = value;
    }
}
