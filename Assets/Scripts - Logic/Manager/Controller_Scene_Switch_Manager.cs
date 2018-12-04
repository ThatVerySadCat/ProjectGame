using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections.Generic;

public class Controller_Scene_Switch_Manager : MonoBehaviour 
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

	void Start () {	}

    void Update() { }

    /// <summary>
    /// Loads the scene with the given ID and sets it as active.
    /// </summary>
    /// <param name="sceneID">The ID of the scene to load and activate.</param>
    public void LoadSceneByID(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
        SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
    }
}
