using UnityEngine;

public class Show2DText : MonoBehaviour
{
    public GameObject textObject; // Asigna aquí el objeto de texto

    private void Start()
    {
        if (textObject != null)
        {
            textObject.SetActive(false); // Oculta el texto al inicio
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga la etiqueta "Player"
        {
            if (textObject != null)
            {
                textObject.SetActive(true); // Muestra el texto
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (textObject != null)
            {
                textObject.SetActive(false); // Oculta el texto cuando el jugador se aleja
            }
        }
    }
}

