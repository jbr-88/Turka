using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntLogic : MonoBehaviour
{
    public float velocity;
    public Transform pointA;
    public Transform pointB;
    public int health = 1; // Número de impactos necesarios para destruir (1 por defecto)
    
    private Rigidbody2D body;
    private Transform target;

    
    
    
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
        
         if (Vector3.Distance(transform.position, target.position) < 0.5f)
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
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= (-1);
        transform.localScale = scale;
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
