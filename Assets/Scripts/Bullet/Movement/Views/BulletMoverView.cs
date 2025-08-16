using UnityEngine;
using VContainer;

public class BulletMoverView : MonoBehaviour
{
    private FieldView _fieldView;
    private BulletSettings _bulletSettings;

    [Inject]
    public void Construct(FieldView fieldView, BulletSettings bulletSettings)
    {
        _fieldView = fieldView;
        _bulletSettings = bulletSettings;
    }

    private void Update()
    {
        Vector3 motion = Time.deltaTime * new Vector3(0f, 0f, _bulletSettings.Speed);
        transform.Translate(motion, Space.World);

        if (transform.position.z > _fieldView.Bounds.extents.z)
            Destroy(gameObject);
    }
}
