using UnityEngine;

using System;
using System.Collections.Generic;

public class Controller_Pause_Overlay : MonoBehaviour 
{
    /// <summary>
    /// Was the game paused the previous frame?
    /// </summary>
    private bool wasPaused = false;
    /// <summary>
    /// A reference to the pause managers script component.
    /// </summary>
    private Controller_Pause_Manager pauseManager;
    /// <summary>
    /// References to the GameObjects that are the children of this object.
    /// </summary>
    private GameObject[] childObjects;

    void Awake()
    {
        pauseManager = GameObject.FindGameObjectWithTag("Pause Manager").GetComponent<Controller_Pause_Manager>();

        childObjects = new GameObject[this.transform.childCount];
        for(int i = 0; i < childObjects.Length; i++)
        {
            childObjects[i] = this.transform.GetChild(i).gameObject;
        }
    }

	void Start () {	}
	
	void LateUpdate ()
    {
        if(wasPaused != pauseManager.IsPaused)
        {
            wasPaused = pauseManager.IsPaused;
            for(int i = 0; i < childObjects.Length; i++)
            {
                childObjects[i].SetActive(wasPaused);
            }
        }
    }
}
