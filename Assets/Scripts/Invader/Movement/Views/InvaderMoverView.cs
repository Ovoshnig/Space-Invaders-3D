using UnityEngine;

public class InvaderMoverView : MonoBehaviour
{
    [SerializeField] private Mesh[] _meshes;

    private MeshFilter _meshFilter;
    private int _meshIndex = 0;

    private MeshFilter MeshFilter
    {
        get
        {
            if (_meshFilter == null)
                _meshFilter = GetComponent<MeshFilter>();

            return _meshFilter;
        }
    }

    public void Move(Vector3 movement)
    {
        transform.Translate(movement);

        _meshIndex++;

        if (_meshIndex >= _meshes.Length)
            _meshIndex = 0;

        MeshFilter.mesh = _meshes[_meshIndex];
    }
}
