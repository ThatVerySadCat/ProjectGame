using UnityEngine;

using System;
using System.Collections.Generic;

public class Controller_Pause_Manager : MonoBehaviour 
{
    public bool IsPaused
    {
        get;
        private set;
    }

    [SerializeField]
    private float pauseDelay = 1.0f;

    private float pauseTimer = 0.0f;

    void Awake()
    {
        pauseTimer = pauseDelay;
    }

	void Start () {	}
	
	void Update ()
    {
        if(pauseTimer < pauseDelay && !IsPaused)
        {
            pauseTimer += Time.deltaTime;
        }
    }

    internal bool Pause()
    {
        if(pauseTimer >= pauseDelay)
        {
            pauseTimer = 0.0f;
            Time.timeScale = 0.0f;
            IsPaused = true;
        }

        return IsPaused;
    }

    internal bool UnPause()
    {
        if(IsPaused)
        {
            Time.timeScale = 1.0f;
            IsPaused = false;
        }

        return !IsPaused;
    }
}
