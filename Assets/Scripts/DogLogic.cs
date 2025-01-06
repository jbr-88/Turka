using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogLogic : MonoBehaviour
{
    public float velocity = 2f;
    public Transform pointA;
    public Transform pointB;
    public int health = 3;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private Rigidbody2D body;
    private Transform target;
    private float jumpTimer = 0f;
    private float jumpInterval = 3f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        target = pointA;
    }

    void FixedUpdate()
    {
        // Movimiento del enemigo
        Vector2 direction = (target.position - transform.position).normalized;
        body.linearVelocity = new Vector2(direction.x * velocity, body.linearVelocityY);

        // Cambiar de dirección al llegar a un punto
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            target = (target == pointA) ? pointB : pointA;
            Flip();
        }

        // Control de salto
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
        scale.x *= -1;
        transform.localScale = scale;
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheckPoint.position, 0.2f, whatIsGround);
    }

    void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocityX, 5f); // 5 es la fuerza del salto, ajusta según lo necesites
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} recibió daño. Vida restante: {health}");

        if (health <= 0)
        {
            Destroy(gameObject);
            Debug.Log($"{gameObject.name} destruido");
        }
    }
}
