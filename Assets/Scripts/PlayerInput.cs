using UnityEngine;
using System.Collections;
using UnityEditor.Tilemaps;

public class PlayerInput : MonoBehaviour
{
    public float walkSpeed;
    public float jumpImpulse;
    public Transform groundCheckPoint;
    public LayerMask whatIsGround;
    public float reboundAfterHitEnemy;
    public float heightGameOver;
    public bool gameOver;
    public bool nextLevel;
    public ChangeScene scene;


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
        nextLevel = false;
    }

    [System.Obsolete]
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

        if(gameObject.transform.position.y < heightGameOver) {
            gameOver = true;
            body.constraints = RigidbodyConstraints2D.FreezeAll;
            scene = FindObjectOfType<ChangeScene>();
            scene.LoadScene("Level1");
        }
    }

    void FixedUpdate()
    {
        movement = body.linearVelocity;
        movement.x = xInput * walkSpeed;
        
        if(jumpInput && iAmOnTheGround) {
            movement.y = jumpImpulse;
        }
        
        body.linearVelocity = movement;
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= (-1);
        transform.localScale = scale;
    }

    [System.Obsolete]
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            //Destroy(collision.gameObject);
            //gameOver = true;
            //body.constraints = RigidbodyConstraints2D.FreezeAll;
            //scene = FindObjectOfType<ChangeScene>();
            //scene.LoadScene("Level1");
        }

        if (collision.gameObject.tag == "nextLevel")
        {
            nextLevel = true;
            body.constraints = RigidbodyConstraints2D.FreezeAll;
            scene = FindObjectOfType<ChangeScene>();
            scene.LoadScene("Level2");
        }
    }
}
