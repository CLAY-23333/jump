using UnityEngine;
using UnityEngine.InputSystem;

public class AutoMove : MonoBehaviour
{
    [Header("移动设置")]
    public float moveSpeed = 5f;
    private Vector3 currentDirection = Vector3.right;

    [Header("跳跃设置")]
    public float jumpForce = 5f;
    public float groundCheckDistance = 1.1f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        transform.Translate(currentDirection * moveSpeed * Time.deltaTime);
        
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            TryJump();
        }
    }

    void TryJump()
    {
        if (Physics.Raycast(transform.position, Vector3.down, groundCheckDistance))
        {

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            Vector3 normal = collision.GetContact(i).normal;
            
            float hitDirection = Vector3.Dot(
                currentDirection.normalized,
                normal
            );

            if (hitDirection < -0.5f)
            {
                FlipDirection();
                return;
            }
        }
    }

    void FlipDirection()
    {
        currentDirection = -currentDirection;
        transform.rotation = Quaternion.identity;
    }
}
