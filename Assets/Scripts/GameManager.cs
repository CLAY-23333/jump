using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("游戏状态")]
    public int targetCount = 0;
    public int currentCount = 0;
    public GameObject winUI;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        if (winUI != null) winUI.SetActive(false);
    }
    
    public void StartNewGame(int totalObjects)
    {
        targetCount = totalObjects;
        currentCount = 0;
        if (winUI != null) winUI.SetActive(false);
    }
    
    public void RegisterArrival()
    {
        currentCount++;

        if (currentCount >= targetCount)
        {
            Win();
        }
    }

    void Win()
    {
        if (winUI != null) winUI.SetActive(true);
    }
}