using UnityEngine;

public class HitOnEnemyLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            // Obtener el script del enemigo y restarle vida
            AntLogic antLogic = collision.GetComponent<AntLogic>();
            CatLogic catLogic = collision.GetComponent<CatLogic>();
            EagleLogic eagleLogic = collision.GetComponent<EagleLogic>();
            DogLogic dogLogic = collision.GetComponent<DogLogic>();

            if (antLogic != null)
            {
                antLogic.TakeDamage(1); // Resta 1 de vida al enemigo
            }else if (catLogic != null)
            {
                catLogic.TakeDamage(1); // Resta 1 de vida al enemigo
            }else if (eagleLogic != null)
            {
                eagleLogic.TakeDamage(1); // Resta 1 de vida al enemigo
            }else if (dogLogic != null)
            {
                dogLogic.TakeDamage(1); // Resta 1 de vida al enemigo
            }
        }
    }
}
