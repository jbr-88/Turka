using System.Collections;
using UnityEngine;

public class StoneLogic : MonoBehaviour
{
    private bool isHeld = false;
    private bool canPickUp = false;
    private Transform holder;
    public float throwForce = 10f;
    public Vector3 offset = new Vector3(0.5f, 0, 0);
    public Vector2 throwDirection = new Vector2(1, 0);
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Kinematic;
    }

    void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.C) && !isHeld)
        {
            PickUp(holder);
        }

        if (isHeld && Input.GetKeyDown(KeyCode.X))
        {
            Throw();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canPickUp = true;
            holder = collision.transform;
            //Debug.Log("Piedra en rango para recoger");
        }

        //Debug.Log($"Piedra impactó con trigger de: {collision.gameObject.name}");

        var antEnemy = collision.gameObject.GetComponent<AntLogic>();
        var eagleEnemy = collision.gameObject.GetComponent<EagleLogic>();
        var dogEnemy = collision.gameObject.GetComponent<DogLogic>();

        if (antEnemy != null)
        {
            //Debug.Log("Impactó con hormiga");
            antEnemy.TakeDamage(1);
            DestroyStone();
            return;
        }
        else if (eagleEnemy != null)
        {
            //Debug.Log("Impactó con águila");
            eagleEnemy.TakeDamage(1);
            DestroyStone();
            return;
        }
        else if (dogEnemy != null)
        {
            //Debug.Log("Impactó con perro");
            dogEnemy.TakeDamage(1);
            DestroyStone();
            return;
        }

        //Debug.Log("Impactó con un objeto que no es un enemigo.");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canPickUp = false;
            //Debug.Log("Piedra fuera de rango");
        }

        
    }

    private void PickUp(Transform player)
    {
        isHeld = true;
        holder = player;

        transform.SetParent(player);
        transform.localPosition = offset;

        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.gravityScale = 0;
        //Debug.Log("Piedra recogida");
    }

    public void Throw()
    {
        if (isHeld)
        {
            isHeld = false;
            transform.SetParent(null);

            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 1;
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(throwDirection.normalized * throwForce, ForceMode2D.Impulse);
            //Debug.Log("Piedra lanzada");

            Invoke(nameof(DestroyStone), 3f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var antEnemy = collision.gameObject.GetComponent<AntLogic>();
        var eagleEnemy = collision.gameObject.GetComponent<EagleLogic>();
        var dogEnemy = collision.gameObject.GetComponent<DogLogic>();

        if (antEnemy != null)
        {
            antEnemy.TakeDamage(1);
            DestroyStone();
            return;
        }
        else if (eagleEnemy != null)
        {
            eagleEnemy.TakeDamage(1);
            DestroyStone();
            return;
        }
        else if (dogEnemy != null)
        {
            dogEnemy.TakeDamage(1);
            DestroyStone();
            return;
        }

        //Debug.Log("La piedra impactó con un objeto que no es un enemigo.");
    }

    private void DestroyStone()
    {
        //Debug.Log("Piedra destruida automáticamente");
        Destroy(gameObject);
    }
}
