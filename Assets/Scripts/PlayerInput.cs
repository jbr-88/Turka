using UnityEngine;
using System.Collections;
using UnityEditor.Tilemaps;

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
    private bool facingRight;
    private Animator anim;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        movement = new Vector2();
        iAmOnTheGround = false;
        anim = GetComponent<Animator>();
        facingRight = true;
    }

    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        jumpInput = Input.GetKey(KeyCode.Space);

        if((xInput < 0) && (facingRight)) {
            Flip();
            facingRight = false;
        }else if((xInput > 0) && (!facingRight)) {
            Flip();
            facingRight = true;
        }

        anim.SetFloat("xSpeed", Mathf.Abs(body.linearVelocityX));
        anim.SetFloat("ySpeed", Mathf.Abs(body.linearVelocityY));
        
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

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= (-1);
        transform.localScale = scale;
    }
}
