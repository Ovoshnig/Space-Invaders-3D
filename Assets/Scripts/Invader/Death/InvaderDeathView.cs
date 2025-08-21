using UnityEngine;

public class InvaderDeathView : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerBulletMoverView>() != null)
        {
            other.gameObject.SetActive(false);
            Destroy(gameObject);
        }
    }
}
