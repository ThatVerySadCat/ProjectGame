using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections.Generic;

public class Controller_Restart_Button : MonoBehaviour 
{
	void Start () {	}
	
	void Update () { }

    /// <summary>
    /// Reloads the current scene, sets it as active and resets the time scale back to 1.
    /// </summary>
    public void RestartScene()
    {
        SceneManager.LoadScene(0);
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));

        Time.timeScale = 1.0f;
    }
}
