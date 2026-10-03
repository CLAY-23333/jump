using UnityEngine;

public class RestartZone : MonoBehaviour
{
    public string playerTag = "AutoMove";
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            TriggerRestart();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(playerTag))
        {
            TriggerRestart();
        }
    }

    private void TriggerRestart()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartCurrentLevel();
        }
    }
}