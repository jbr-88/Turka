using System.Collections;
using UnityEngine;

public class StoneLogic : MonoBehaviour
{
    private bool isHeld = false; // Si la piedra está siendo sostenida
    private Transform holder; // Transform del personaje que sostiene la piedra

    public float throwForce = 10f; // Fuerza de lanzamiento
    public Vector3 offset = new Vector3(0.5f, 0.5f, 0);
    public Vector2 throwDirection = new Vector2(1, 0); // Dirección de lanzamiento (hacia la derecha por defecto)

    private Rigidbody2D rb; // Referencia al Rigidbody2D

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Detectar la tecla de lanzar mientras la piedra está siendo sostenida
        if (isHeld && Input.GetKeyDown(KeyCode.X))
        {
            Throw();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Detecta si el jugador entra en el rango
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Piedra disponible para recoger");
        }


        Debug.Log($"La piedra colisionó con: {collision.gameObject.name}");

        // Verifica si el objeto tiene un script específico
        var antEnemy = collision.gameObject.GetComponent<AntLogic>();
        var eagleEnemy = collision.gameObject.GetComponent<EagleLogic>();
        var dogEnemy = collision.gameObject.GetComponent<DogLogic>();

        if (antEnemy != null)
        {
            antEnemy.TakeDamage(1); // Aplica 1 punto de daño
            Destroy(gameObject); // Destruye la piedra después del impacto
            return;
        }else if (eagleEnemy != null) {
            eagleEnemy.TakeDamage(1); // Aplica 1 punto de daño
            Destroy(gameObject); // Destruye la piedra después del impacto
            return;
        }

        
        if (dogEnemy != null)
        {
            dogEnemy.TakeDamage(1); // Aplica 1 punto de daño
            Destroy(gameObject); // Destruye la piedra después del impacto
            return;
        }
        
        Debug.Log("La piedra impactó con un objeto que no es un enemigo.");
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // Detecta si el jugador presiona el botón para recoger
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.C) && !isHeld)
        {
            PickUp(collision.transform);
        }
    }

    private void PickUp(Transform player)
    {
        isHeld = true;
        holder = player;

        // Fijar la piedra al jugador
        transform.SetParent(player);
        transform.localPosition = offset; // Establecer la posición local de la piedra cerca del jugador

        // Desactivar la gravedad mientras la piedra esté siendo sostenida
        rb.bodyType = RigidbodyType2D.Kinematic; // Desactiva las físicas
        rb.gravityScale = 0; // Desactiva la gravedad
        Debug.Log("Piedra recogida");
    }

    public void Throw()
    {
        if (isHeld)
        {
            isHeld = false;
            transform.SetParent(null);

            // Reactivar las físicas y la gravedad al lanzar
            rb.bodyType = RigidbodyType2D.Dynamic; // Reactiva las físicas
            rb.gravityScale = 1; // Reactiva la gravedad

            // Asegurarse de que la dirección esté correcta y lanzar en línea recta
            rb.linearVelocity = Vector2.zero; // Restablecer la velocidad a cero antes de lanzar
            rb.AddForce(throwDirection.normalized * throwForce, ForceMode2D.Impulse); // Aplica la fuerza de lanzamiento
            Debug.Log("Piedra lanzada en línea recta");

            // Programar la destrucción automática de la piedra después de 5 segundos
            Invoke(nameof(DestroyStone), 3f);
        }
    }

    private void DestroyStone()
    {
        Debug.Log("Piedra destruida automáticamente");
        Destroy(gameObject);
    }
}