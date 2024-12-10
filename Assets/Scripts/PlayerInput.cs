using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    public float walkSpeed;
    public float jumpImpulse;
    //public float maxFallSpeed;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;

    private Rigidbody2D body;
    private Vector2 movement;
    private float xInput;
    private bool jumpInput;
    private bool iAmOnTheGround;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        movement = new Vector2();
        iAmOnTheGround = false;
    }

    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetKey(KeyCode.Space);
        
        if(Physics2D.OverlapCircle(groundCheckPoint.position, 0.02f, whatIsGround)) {
            iAmOnTheGround = true;
        }else {
            iAmOnTheGround = false;
        }
    }

    void FixedUpdate()
    {
        movement = body.linearVelocity;
        movement.x = xInput * walkSpeed;
        
        if(jumpInput && iAmOnTheGround) {
            movement.y = jumpImpulse;
        }
        /*
        if(!iAmOnTheGround) {
            if(movement.y < maxFallSpeed) {
                movement.y = maxFallSpeed;
            }
        }
        */
        body.linearVelocity = movement;
    }
}
