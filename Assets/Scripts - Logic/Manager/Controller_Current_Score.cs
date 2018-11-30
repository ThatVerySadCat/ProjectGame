using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;

public class Controller_Current_Score : MonoBehaviour 
{
    /// <summary>
    /// A reference to the score managers script component.
    /// </summary>
    private Controller_Score_Manager scoreManager;
    /// <summary>
    /// A reference to the local Text component.
    /// </summary>
    private Text currentScoreText;

    void Awake()
    {
        scoreManager = GameObject.FindGameObjectWithTag("Score Manager").GetComponent<Controller_Score_Manager>();
        currentScoreText = this.GetComponent<Text>();
    }

	void Start () {	}
	
	void Update ()
    {
        currentScoreText.text = scoreManager.CurrentScore.ToString();
    }
}
