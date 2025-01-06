using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogLogic : MonoBehaviour
{
    public float velocity;
    public Transform pointA;
    public Transform pointB;
    public int health = 3;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private Rigidbody2D body;
    private Transform target;
    private float jumpTimer = 0f;
    private float jumpInterval = 2f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        target = pointA;
    }

    void FixedUpdate()
    {
        // Movimiento del enemigo con velocidad constante
        float moveDirection = Mathf.Sign(target.position.x - transform.position.x);
        body.linearVelocity = new Vector2(moveDirection * velocity, body.linearVelocityY);

        // Cambiar de dirección al llegar a un punto (umbral aumentado a 0.7f)
        if (Mathf.Abs(transform.position.x - target.position.x) < 0.7f)
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
        body.linearVelocity = new Vector2(body.linearVelocityX, 5f); // Mantener la velocidad horizontal y solo modificar la vertical
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
