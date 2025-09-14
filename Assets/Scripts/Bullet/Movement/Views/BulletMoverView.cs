using R3;
using UnityEngine;

public abstract class BulletMoverView : MonoBehaviour
{
    public enum DirectionZ
    {
        Forward = 1,
        Backward = -1
    }

    [SerializeField] private BulletExplosionView _explosionViewPrefab;
    [SerializeField] private GameSettings _gameSettings;

    private readonly ReactiveProperty<bool> _isEnabled = new();

    private float _extentsZ;

    public ReadOnlyReactiveProperty<bool> IsEnabled => _isEnabled;

    protected GameSettings GameSettings => _gameSettings;
    protected abstract DirectionZ Direction { get; }
    protected abstract float Speed { get; }

    private void Awake() => _extentsZ = GetComponent<BoxCollider>().bounds.extents.z;

    private void OnEnable() => _isEnabled.Value = true;

    private void OnDisable() => _isEnabled.Value = false;

    private void Update()
    {
        if (!enabled)
            return;

        FieldSettings fieldSettings = _gameSettings.FieldSettings;
        float fieldMinZ = fieldSettings.Min.z;
        float fieldMaxZ = fieldSettings.Max.z;

        float positionZ = transform.position.z;
        float deltaZ = (int)Direction * Time.deltaTime * Speed;
        float endPositionZ = positionZ + deltaZ;

        if (endPositionZ - _extentsZ >= fieldMinZ
            && endPositionZ + _extentsZ <= fieldMaxZ)
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
