using UnityEngine;

using System;
using System.Collections.Generic;

public class Controller_Difficulty_Manager : MonoBehaviour 
{
    /// <summary>
    /// Has the current difficulty changed this frame?
    /// </summary>
    public bool HasDifficultyChanged
    {
        get;
        private set;
    }
    /// <summary>
    /// The amount of time the manager has to wait before increasing the difficulty, in Seconds.
    /// </summary>
    public float DifficultyIncreaseDelay
    {
        get
        {
            return difficultyIncreaseDelay;
        }
    }
    /// <summary>
    /// The time since the difficulty was last increased, in Seconds.
    /// </summary>
    public float DifficultyIncreaseTimer
    {
        get
        {
            return difficultyIncreaseTimer;
        }
    }
    /// <summary>
    /// The number the difficulty is currently at.
    /// </summary>
    public int CurrentDifficulty
    {
        get;
        private set;
    }
    
    [SerializeField, Tooltip("The amount of time the manager has to wait before increasing the difficulty, in Seconds")]
    private float difficultyIncreaseDelay = 1.0f;
    [SerializeField, Tooltip("The number at which to start the difficulty counter.")]
    private int startingDifficulty = 1;

    /// <summary>
    /// The time since the difficulty was last increased, in Seconds.
    /// </summary>
    private float difficultyIncreaseTimer = 0.0f;

    void Awake()
    {
        CurrentDifficulty = startingDifficulty;
    }

	void Start () {	}
	
	void Update ()
    {
        if(HasDifficultyChanged)
        {
            HasDifficultyChanged = false;
        }

        difficultyIncreaseTimer += Time.deltaTime;
        if(difficultyIncreaseTimer >= difficultyIncreaseDelay)
        {
            difficultyIncreaseTimer = 0.0f;

            CurrentDifficulty += 1;

            HasDifficultyChanged = true;
        }
    }
}
