using UnityEngine;

public class UFOMoverView : MonoBehaviour
{
    private void Start() => gameObject.SetActive(false);

    public void StartMovement(Vector3 startPosition)
    {
        transform.position = startPosition;
        gameObject.SetActive(true);
    }

    public void StopMovement() => gameObject.SetActive(false);

    public void Move(Vector3 movement) => transform.Translate(movement);
}
