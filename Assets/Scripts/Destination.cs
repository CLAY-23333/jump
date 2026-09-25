using UnityEngine;

public class Destination : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AutoMove"))
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.RegisterArrival();
            }

            Destroy(other.gameObject);
        }
    }
}