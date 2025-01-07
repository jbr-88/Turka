using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleLogic : MonoBehaviour
{
    public float velocity;
    public Transform pointA;
    public Transform pointB;
    public int health = 1;
    
    private Rigidbody2D body;
    private Transform target;

    
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        target = pointA;
    }

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
        health -= damage;
        //Debug.Log($"{gameObject.name} recibió daño. Vida restante: {health}");

        if (health <= 0)
        {
            Destroy(gameObject);
            //Debug.Log($"{gameObject.name} destruido");
        }
    }
}
