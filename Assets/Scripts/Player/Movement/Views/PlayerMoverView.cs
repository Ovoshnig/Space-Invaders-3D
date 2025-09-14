using UnityEngine;

public class PlayerMoverView : MonoBehaviour
{
    [SerializeField] private GameSettings _gameSettings;

    private BoxCollider _boxCollider = null;

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

        FieldSettings fieldSettings = _gameSettings.FieldSettings;
        float minX = fieldSettings.Min.x + halfWidth;
        float maxX = fieldSettings.Max.x - halfWidth;

        Vector3 nextPosition = transform.position + movement;
        nextPosition.x = Mathf.Clamp(nextPosition.x, minX, maxX);
        transform.position = nextPosition;
    }
}
