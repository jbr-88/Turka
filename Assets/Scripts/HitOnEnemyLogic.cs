using UnityEngine;

public class HitOnEnemyLogic : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Destroy(collision.gameObject);
            //gameOver = true;
            //body.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}
