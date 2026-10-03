using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("游戏状态")]
    public int targetCount = 0;
    public int currentCount = 0;

    [Header("Win UI")]
    public GameObject winUI;

    private bool levelCompleted = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    private void Update()
    {
        if (Keyboard.current != null && Keyboard.current.rKey.wasPressedThisFrame)
        {
            RestartCurrentLevel();
        }
    }
    
    private void Start()
    {
        if (winUI != null)
        {
            winUI.SetActive(false);
        }
    }
    public void StartNewGame(int totalObjects)
    {
        targetCount = totalObjects;
        currentCount = 0;
        levelCompleted = false;

        if (winUI != null)
        {
            winUI.SetActive(false);
        }

        Debug.Log("Level started. Total blocks: " + targetCount);
    }
    public void RegisterArrival()
    {
        if (levelCompleted)
            return;

        currentCount++;

        Debug.Log("Blocks arrived: " + currentCount + " / " + targetCount);

        if (currentCount >= targetCount)
        {
            levelCompleted = true;
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int lastScene = SceneManager.sceneCountInBuildSettings - 1;

        if (currentScene < lastScene)
        {
            SceneManager.LoadScene(currentScene + 1);
        }
        else
        {
            if (winUI != null)
            {
                winUI.SetActive(true);
            }
            else
            {
                Debug.LogWarning("Win UI is not assigned!");
            }
        }
    }
    
    public void RestartCurrentLevel()
    {
        Time.timeScale = 1f;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}