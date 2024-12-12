using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntLogic : MonoBehaviour
{
    public float velocity;

    public Rigidbody2D body;

    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        body.linearVelocity = new Vector2(-velocity, body.linearVelocityY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "invisibleWall") {
            velocity *= (-1);
            Flip();
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= (-1);
        transform.localScale = scale;
    }
}
