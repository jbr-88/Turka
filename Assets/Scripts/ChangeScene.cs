using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//For use scene management commands
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    //Load the scene with the specific name
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
