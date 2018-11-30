using UnityEngine;

using System;
using System.Collections.Generic;

public class Controller_Score_Manager : MonoBehaviour 
{
    /// <summary>
    /// The score that has been obtained so far.
    /// </summary>
    public float CurrentScore
    {
        get
        {
            return currentScore;
        }
    }

    /// <summary>
    /// A reference to the difficulty managers script component.
    /// </summary>
    private Controller_Difficulty_Manager difficultyManager;
    /// <summary>
    /// The time since the last amount of score was added, in Seconds.
    /// </summary>
    private float scoreTimer = 0.0f;
    /// <summary>
    /// The score that has been obtained so far.
    /// </summary>
    private int currentScore = 0;
    /// <summary>
    /// The Score DAL interface to use for DAL communication.
    /// </summary>
    private IScoreDAL scoreDAL;

    void Awake()
    {
        difficultyManager = GameObject.FindGameObjectWithTag("Difficulty Manager").GetComponent<Controller_Difficulty_Manager>();
    }

	void Start () {	}

    void Update()
    {
        scoreTimer += Time.deltaTime;
        if (scoreTimer >= 1.0f)
        {
            scoreTimer -= 1.0f;

            currentScore += 1 * difficultyManager.CurrentDifficulty;
        }
    }
}
