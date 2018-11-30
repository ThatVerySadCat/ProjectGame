using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;

public class Controller_Player_Dead_Overlay : MonoBehaviour 
{
    [SerializeField, Tooltip("The speed with which the black background fades in, in Seconds.")]
    private float fadeInSpeed = 1.0f;

    /// <summary>
    /// Has the black background faded in fully?
    /// </summary>
    private bool hasFadedIn = false;
    /// <summary>
    /// A reference to the after death score overlays script component.
    /// </summary>
    private Controller_After_Death_Score_Overlay afterDeathScoreOverlay;
    /// <summary>
    /// A reference to the players script component.
    /// </summary>
    private Controller_Player player;
    /// <summary>
    /// The current alpha for child objects.
    /// </summary>
    private float currentAlpha = 0.0f;
    /// <summary>
    /// A reference to the backgrounds Image component.
    /// </summary>
    private Image backgroundImage;
    /// <summary>
    /// A reference to the you have dieds Text component.
    /// </summary>
    private Text youHaveDiedText;

    void Awake()
    {
        afterDeathScoreOverlay = GameObject.FindGameObjectWithTag("After Death Score Overlay").GetComponent<Controller_After_Death_Score_Overlay>();
        afterDeathScoreOverlay.gameObject.SetActive(false);

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller_Player>();
        backgroundImage = this.transform.GetChild(0).GetComponent<Image>();
        youHaveDiedText = this.transform.GetChild(1).GetComponent<Text>();
    }

	void Start () {	}
	
	void Update ()
    {
        if(player.CurrentHealth <= 0.0f && !hasFadedIn)
        {
            currentAlpha = Mathf.Clamp01(currentAlpha + fadeInSpeed * Time.unscaledDeltaTime);
            backgroundImage.color = UpdateAlpha(backgroundImage.color, currentAlpha);
            youHaveDiedText.color = UpdateAlpha(youHaveDiedText.color, currentAlpha);

            if(currentAlpha >= 1.0f)
            {
                afterDeathScoreOverlay.gameObject.SetActive(true);
                Time.timeScale = 0.0f;
                hasFadedIn = true;
            }
        }
    }

    /// <summary>
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Uses the given oldColor and sets its alpha to the given newAlpha before returning it.
    /// </summary>
    /// <param name="oldColor">The color whose alpha channel to change.</param>
    /// <param name="newAlpha">The alpha amount to change to.</param>
    /// <returns>A new Color with the given newAlpha.</returns>
    internal Color UpdateAlpha(Color oldColor, float newAlpha)
    {
        Color returnColor = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a);
        returnColor.a = newAlpha;
        return returnColor;
    }
}
