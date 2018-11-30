using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;

public class Controller_Difficulty_Tracker : MonoBehaviour 
{
    /// <summary>
    /// A reference to the difficulty managers script component.
    /// </summary>
    private Controller_Difficulty_Manager difficultyManager;
    /// <summary>
    /// A reference to the second childs Image component.
    /// </summary>
    private Image difficultyProgressImage;
    /// <summary>
    /// A reference to the first childs Text component.
    /// </summary>
    private Text currentDifficultyText;

    void Awake()
    {
        difficultyManager = GameObject.FindGameObjectWithTag("Difficulty Manager").GetComponent<Controller_Difficulty_Manager>();
        difficultyProgressImage = this.transform.GetChild(1).GetComponent<Image>();
        currentDifficultyText = this.transform.GetChild(0).GetComponent<Text>();
    }

	void Start ()
    {
        currentDifficultyText.text = difficultyManager.CurrentDifficulty + "x";
    }
	
	void Update ()
    {
        if(difficultyManager.HasDifficultyChanged)
        {
            currentDifficultyText.text = difficultyManager.CurrentDifficulty + "x";
        }
        
        difficultyProgressImage.fillAmount = Mathf.Clamp01(difficultyManager.DifficultyIncreaseTimer / difficultyManager.DifficultyIncreaseDelay);
    }
}
