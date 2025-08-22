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

    private readonly ReactiveProperty<bool> _isEnabled = new();

    private BulletSettings _bulletSettings;
    private float _fieldMinZ;
    private float _fieldMaxZ;
    private float _extentsZ;

    [Inject]
    public void Construct(BulletSettings bulletSettings, FieldView fieldView)
    {
        _bulletSettings = bulletSettings;
        _fieldMinZ = fieldView.Bounds.min.z;
        _fieldMaxZ = fieldView.Bounds.max.z;
        _extentsZ = GetComponent<MeshRenderer>().bounds.extents.z;
    }

    public ReadOnlyReactiveProperty<bool> IsEnabled => _isEnabled;

    protected abstract DirectionZ Direction { get; }

    private void OnEnable() => _isEnabled.Value = true;

    private void OnDisable() => _isEnabled.Value = false;

    private void Update()
    {
        if (!enabled)
            return;

        float positionZ = transform.position.z;
        float deltaZ = (int)Direction * Time.deltaTime * _bulletSettings.Speed;
        float endPositionZ = positionZ + deltaZ;

        if (endPositionZ - _extentsZ >= _fieldMinZ
            && endPositionZ + _extentsZ <= _fieldMaxZ)
        {
            Vector3 movement = new(0f, 0f, deltaZ);
            transform.Translate(movement, Space.World);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
