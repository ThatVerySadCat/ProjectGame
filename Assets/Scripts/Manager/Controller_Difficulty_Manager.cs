using UnityEngine;

using System;
using System.Collections.Generic;

public class Controller_Difficulty_Manager : MonoBehaviour 
{
    public bool ChangedThisFrame
    {
        get;
        private set;
    }
    public int CurrentDifficulty
    {
        get;
        private set;
    }
    
    [SerializeField]
    private float difficultyIncreaseDelay = 1.0f;

    private float difficultyIncreaseTimer = 0.0f;

	void Start () {	}
	
	void Update ()
    {
        if(ChangedThisFrame)
        {
            ChangedThisFrame = false;
        }

        difficultyIncreaseTimer += Time.deltaTime;
        if(difficultyIncreaseTimer >= difficultyIncreaseDelay)
        {
            difficultyIncreaseTimer = 0.0f;

            CurrentDifficulty += 1;

            ChangedThisFrame = true;
        }
    }
}
