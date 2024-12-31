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
    public int health = 1; // Número de impactos necesarios para destruir (1 por defecto)
    
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
        // Movimiento
        Vector2 direction = (target.position - transform.position).normalized;
        body.linearVelocity = new Vector2(direction.x * velocity, body.linearVelocityY); // Mantén la velocidad vertical

        // Cambiar de dirección al alcanzar un objetivo
        if (Vector3.Distance(transform.position, target.position) < 0.6f)
        {
            target = (target == pointA) ? pointB : pointA;
            Flip();
        }

        // Salto
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
        // Dibuja un círculo para comprobar si el enemigo está en el suelo
        bool grounded = Physics2D.OverlapCircle(groundCheckPoint.position, 1f, whatIsGround);
        Debug.Log($"IsGrounded: {grounded}");
        return grounded;
    }

    void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocityX, jumpForce); // Aplica solo la fuerza vertical
        Debug.Log($"Jump! Velocity: {body.linearVelocity}");
    }


    public void TakeDamage(int damage)
    {
        health -= damage; // Reduce la vida
        Debug.Log($"{gameObject.name} recibió daño. Vida restante: {health}");

        if (health <= 0)
        {
            Destroy(gameObject); // Destruye el enemigo si su vida llega a 0
            Debug.Log($"{gameObject.name} destruido");
        }
    }
}
