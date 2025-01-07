using UnityEngine;

public class Show2DText : MonoBehaviour
{
    public GameObject textObject;

    private void Start()
    {
        if (textObject != null)
        {
            textObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (textObject != null)
            {
                textObject.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (textObject != null)
            {
                textObject.SetActive(false);
            }
        }
    }
}

