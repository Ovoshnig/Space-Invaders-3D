using UnityEngine;

public class UFOMoverView : MonoBehaviour
{
    public void StartMovement(Vector3 startPosition)
    {
        transform.position = startPosition;
        gameObject.SetActive(true);
    }

    public void StopMovement() => gameObject.SetActive(false);

    public void Move(Vector3 movement) => transform.Translate(movement);
}
