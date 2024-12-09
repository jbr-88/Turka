using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    public float walkSpeed;
    public float jumpImpulse;

    private Rigidbody2D body;
    private Vector2 movement;
    private float xInput;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        movement = new Vector2();
    }

    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
    }

    void FixedUpdate()
    {
        movement = body.linearVelocity;
        movement.x = xInput * walkSpeed;
        body.linearVelocity = movement;
    }
}
