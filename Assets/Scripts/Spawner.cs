using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Spawner : MonoBehaviour
{
    [Header("设置")]
    public GameObject objectPrefab;
    public int spawnCount = 5;
    public float spawnInterval = 0.5f;
    
    [Header("相机追踪设置")]
    public CinemachineTargetGroup targetGroup;
    public float targetWeight = 1f;
    public float targetRadius = 2f;
    private bool isSpawning = false;
    private Collider spawnerCollider;
    private bool hasTriggered = false;
    
    void Start()
    {
        spawnerCollider = GetComponent<Collider>();
    }
    
    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame && !isSpawning && !hasTriggered)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    IEnumerator SpawnRoutine()
    {
        isSpawning = true;
        hasTriggered = true;
        
        if (GameManager.Instance != null)
        {
            GameManager.Instance.StartNewGame(spawnCount);
        }

        for (int i = 0; i < spawnCount; i++)
        {
            SpawnOneObject();
            yield return new WaitForSeconds(spawnInterval);
        }

        isSpawning = false;
    }
    
    void SpawnOneObject()
    {
        GameObject newObj = Instantiate(objectPrefab, transform.position, Quaternion.identity);
        targetGroup.AddMember(newObj.transform, targetWeight, targetRadius);
        Collider objCollider = newObj.GetComponent<Collider>();
        
        if (spawnerCollider != null && objCollider != null)
        {
            Physics.IgnoreCollision(spawnerCollider, objCollider, true);
            
            StartCoroutine(ReEnableCollisionWhenLeft(objCollider));
        }
    }
    
    IEnumerator ReEnableCollisionWhenLeft(Collider objCollider)
    {
        while (objCollider != null && spawnerCollider.bounds.Intersects(objCollider.bounds))
        {
            yield return null;
        }

        if (objCollider != null)
        {
            Physics.IgnoreCollision(spawnerCollider, objCollider, false);
        }
    }
}
