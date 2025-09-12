using UnityEngine;

public class ShieldTileView : MonoBehaviour
{
    [SerializeField] private Mesh[] _meshes;

    private MeshFilter _meshFilter = null;
    private int _index = 0;

    private MeshFilter MeshFilter
    {
        get
        {
            if (_meshFilter == null)
                _meshFilter = GetComponent<MeshFilter>();

            return _meshFilter;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BulletMoverView bulletView))
        {
            bulletView.gameObject.SetActive(false);

            _index++;

            if (_index >= _meshes.Length)
            {
                gameObject.SetActive(false);
                return;
            }

            MeshFilter.mesh = _meshes[_index];
        }
    }

    public void Restore()
    {
        if (this == null)
            return;

        _index = 0;
        MeshFilter.mesh = _meshes[_index];
        gameObject.SetActive(true);
    }
}
