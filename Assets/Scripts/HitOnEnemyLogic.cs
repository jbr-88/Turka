using UnityEngine;

public class HitOnEnemyLogic : MonoBehaviour
{
    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemy") || collision.CompareTag("finalBoss"))
        {
            AntLogic antLogic = collision.GetComponent<AntLogic>();
            EagleLogic eagleLogic = collision.GetComponent<EagleLogic>();
            DogLogic dogLogic = collision.GetComponent<DogLogic>();

            if (antLogic != null)
            {
                antLogic.TakeDamage(1);
            }
            else if (eagleLogic != null)
            {
                eagleLogic.TakeDamage(1);
            }
            else if (dogLogic != null)
            {
                dogLogic.TakeDamage(1);
            }

            PlayerInput player = FindObjectOfType<PlayerInput>();
            if (player != null)
            {
                player.Rebound(10f);
            }
        }
    }


    public void Rebound(float reboundForce)
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        body.AddForce(Vector2.up * reboundForce, ForceMode2D.Impulse);
    }

}
