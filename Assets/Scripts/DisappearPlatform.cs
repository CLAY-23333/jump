using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearPlatform : MonoBehaviour
{
    [Header("消失设置")]
    public float destroyDelay = 0.2f;

    private readonly HashSet<GameObject> objectsOnPlatform = new();

    private bool hasBeenTouched;
    private bool isDestroying;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject mover = GetAutoMoveObject(collision);

        if (mover == null)
            return;

        hasBeenTouched = true;
        objectsOnPlatform.Add(mover);
    }

    private void OnCollisionExit(Collision collision)
    {
        GameObject mover = GetAutoMoveObject(collision);

        if (mover == null)
            return;

        objectsOnPlatform.Remove(mover);
        
        if (hasBeenTouched &&
            objectsOnPlatform.Count == 0 &&
            !isDestroying)
        {
            StartCoroutine(DestroyPlatform());
        }
    }

    private GameObject GetAutoMoveObject(Collision collision)
    {
        GameObject target = collision.rigidbody != null
            ? collision.rigidbody.gameObject
            : collision.gameObject;

        return target.CompareTag("AutoMove") ? target : null;
    }

    private IEnumerator DestroyPlatform()
    {
        isDestroying = true;

        yield return new WaitForSeconds(destroyDelay);

        Destroy(gameObject);
    }
}