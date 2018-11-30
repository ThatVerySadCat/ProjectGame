using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;

public class Controller_After_Death_Score_Overlay : MonoBehaviour 
{
    /// <summary>
    /// A reference to the score managers script component.
    /// </summary>
    private Controller_Score_Manager scoreManager;
    /// <summary>
    /// A reference to the child scores Text component.
    /// </summary>
    private Text scoreText;

    void Awake()
    {
        scoreManager = GameObject.FindGameObjectWithTag("Score Manager").GetComponent<Controller_Score_Manager>();
        scoreText = this.transform.GetChild(2).GetComponent<Text>();
    }

	void Start () {	}
	
	void Update ()
    {
        scoreText.text = scoreManager.CurrentScore.ToString();
    }
}
