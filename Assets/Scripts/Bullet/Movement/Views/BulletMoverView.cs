using R3;
using UnityEngine;
using VContainer;

public abstract class BulletMoverView : MonoBehaviour
{
    public enum DirectionZ
    {
        Forward = 1,
        Backward = -1
    }

    [SerializeField] private BulletExplosionView _explosionViewPrefab;

    private readonly ReactiveProperty<bool> _isEnabled = new();

    private float _fieldMinZ;
    private float _fieldMaxZ;
    private float _extentsZ;

    [Inject]
    public void Construct(FieldSettings fieldSettings)
    {
        _fieldMinZ = fieldSettings.Min.z;
        _fieldMaxZ = fieldSettings.Max.z;
        _extentsZ = GetComponent<BoxCollider>().bounds.extents.z;
    }

    public ReadOnlyReactiveProperty<bool> IsEnabled => _isEnabled;

    protected abstract DirectionZ Direction { get; }
    protected abstract float Speed { get; }

    private void OnEnable() => _isEnabled.Value = true;

    private void OnDisable() => _isEnabled.Value = false;

    private void Update()
    {
        if (!enabled)
            return;

        float positionZ = transform.position.z;
        float deltaZ = (int)Direction * Time.deltaTime * Speed;
        float endPositionZ = positionZ + deltaZ;

        if (endPositionZ - _extentsZ >= _fieldMinZ
            && endPositionZ + _extentsZ <= _fieldMaxZ)
        {
            Vector3 movement = new(0f, 0f, deltaZ);
            transform.Translate(movement, Space.World);
        }
        else
        {
            Instantiate(_explosionViewPrefab, transform.position, Quaternion.identity);

            gameObject.SetActive(false);
        }
    }
}
