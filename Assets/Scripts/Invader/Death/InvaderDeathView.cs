using UnityEngine;

public class InvaderDeathView : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<BulletMoverView>() != null)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
