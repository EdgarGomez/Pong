using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonActions : MonoBehaviour
{
    public void LoadPong()
    {
        SceneManager.LoadScene(1);
    }

    public void QuiteGame()
    {
        Application.Quit();
        // Only for dev purposes - Remove
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
