using UnityEngine;
using UnityEngine.UI; // Necesario para trabajar con la UI

public class ExitGameButton : MonoBehaviour
{
    public void OnExitButtonPressed()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
