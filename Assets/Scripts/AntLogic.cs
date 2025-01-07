using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntLogic : MonoBehaviour
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
        Vector2 direction = (target.position - transform.position).normalized;
        body.linearVelocity = direction * velocity;
        
         if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            if (target == pointA)
            {
                target = pointB;
                Flip();
            }
            else
            {
                target = pointA;
                Flip();
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
        health -= damage;
        //Debug.Log($"{gameObject.name} recibió daño. Vida restante: {health}");

        if (health <= 0)
        {
            Destroy(gameObject);
            //Debug.Log($"{gameObject.name} destruido");
        }
    }
}
