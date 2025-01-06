using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con la UI

public class ExitGameButton : MonoBehaviour
{
    // Método que se llama cuando se presiona el botón
    public void OnExitButtonPressed()
    {
        // Si estamos en el editor de Unity, simplemente detener la ejecución
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            // En una compilación final, se cierra la aplicación
            Application.Quit();
        #endif
    }
}
