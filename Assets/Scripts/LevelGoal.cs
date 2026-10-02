using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelGoal : MonoBehaviour
{
    [SerializeField] private GameObject winCanvas;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int lastScene = SceneManager.sceneCountInBuildSettings - 1;

        if (currentScene < lastScene)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
        else
        {
            winCanvas.SetActive(true);
        }
    }
}
