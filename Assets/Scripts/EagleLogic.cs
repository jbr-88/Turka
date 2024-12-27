using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleLogic : MonoBehaviour
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
        Vector2 direction = new Vector2(0, target.position.y - transform.position.y).normalized;
        body.linearVelocity = direction * velocity;
        
        if (Mathf.Abs(transform.position.y - target.position.y) < 0.5f)
        {
            target = (target == pointA) ? pointB : pointA;
        }
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
