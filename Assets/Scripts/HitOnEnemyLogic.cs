using UnityEngine;

public class HitOnEnemyLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy"))
        {
            // Obtener el script del enemigo y restarle vida
            AntLogic antLogic = collision.GetComponent<AntLogic>();
            EagleLogic eagleLogic = collision.GetComponent<EagleLogic>();
            DogLogic dogLogic = collision.GetComponent<DogLogic>();

            if (antLogic != null)
            {
                antLogic.TakeDamage(1); // Resta 1 de vida al enemigo
            }else
            {
                eagleLogic.TakeDamage(1); // Resta 1 de vida al enemigo
            }
        }
    }
}
