using UnityEngine;

using System;
using System.Collections.Generic;

public class Controller_Pause_Manager : MonoBehaviour 
{
    /// <summary>
    /// Is the game paused?
    /// </summary>
    public bool IsPaused
    {
        get;
        private set;
    }

    [SerializeField, Tooltip("The minimum time that must've passed in between two pauses, in Seconds.")]
    private float pauseDelay = 1.0f;

    /// <summary>
    /// The time since the last pause, in Seconds.
    /// </summary>
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

    /// <summary>
    /// Pauses the game and returns true. Returns false otherwise.
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Unpauses the game and returns true. Returns false otherwise.
    /// </summary>
    /// <returns></returns>
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
