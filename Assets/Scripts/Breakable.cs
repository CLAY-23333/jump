using UnityEngine;

public class Breakable : MonoBehaviour
{
    public float destroyDelay = 0.05f; 

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("AutoMove"))
        {
            Destroy(gameObject, destroyDelay);
        }
    }
}