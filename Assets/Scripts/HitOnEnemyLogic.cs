using UnityEngine;

public class HitOnEnemyLogic : MonoBehaviour
{
    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
{
    if (collision.CompareTag("enemy") || collision.CompareTag("finalBoss"))
    {
        // Obtener el script del enemigo y restarle vida
        AntLogic antLogic = collision.GetComponent<AntLogic>();
        EagleLogic eagleLogic = collision.GetComponent<EagleLogic>();
        DogLogic dogLogic = collision.GetComponent<DogLogic>();

        if (antLogic != null)
        {
            antLogic.TakeDamage(1); // Resta 1 de vida al enemigo
        }
        else if (eagleLogic != null)
        {
            eagleLogic.TakeDamage(1); // Resta 1 de vida al enemigo
        }
        else if (dogLogic != null)
        {
            dogLogic.TakeDamage(1); // Resta 1 de vida al enemigo
        }

        // Buscar al jugador y aplicar el rebote
        PlayerInput player = FindObjectOfType<PlayerInput>();
        if (player != null)
        {
            player.Rebound(10f); // Llama al método de rebote del jugador con una fuerza de 5
        }
    }
}


    public void Rebound(float reboundForce)
{
    Rigidbody2D body = GetComponent<Rigidbody2D>();
    body.AddForce(Vector2.up * reboundForce, ForceMode2D.Impulse);
}

}
