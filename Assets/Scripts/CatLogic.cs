using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatLogic : MonoBehaviour
{
    public float velocity;
    public float jumpForce;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public Transform pointA;
    public Transform pointB;
    
    private Rigidbody2D body;
    private Transform target;
    private float jumpTimer = 0f; // Temporizador para el salto
    private float jumpInterval = 2f; // Intervalo entre saltos
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        target = pointA;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        body.linearVelocity = direction * velocity;
        
         if (Vector3.Distance(transform.position, target.position) < 0.6f)
        {
            if (target == pointA)
            {
                target = pointB;
                Flip(); // Girar el sprite
            }
            else
            {
                target = pointA;
                Flip(); // Girar el sprite
            }
        }

        jumpTimer += Time.fixedDeltaTime;
        if (jumpTimer >= jumpInterval && IsGrounded())
        {
            Jump();
            jumpTimer = 0f;
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= (-1);
        transform.localScale = scale;
    }

    bool IsGrounded()
    {
        // Comprueba si hay contacto con el suelo mediante un círculo
        return Physics2D.OverlapCircle(groundCheckPoint.position, 0.1f, whatIsGround);
    }

    void Jump()
    {
        body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}
