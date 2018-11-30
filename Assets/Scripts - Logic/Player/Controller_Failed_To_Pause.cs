using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;

public class Controller_Failed_To_Pause : MonoBehaviour 
{
    [SerializeField, Tooltip("The amount of time the text remains visible before starting to fade out, in Seconds.")]
    private float fadeOutDelay = 1.0f;
    [SerializeField, Tooltip("The speed at which the text is faded out, in Alpha/Second.")]
    private float fadeOutSpeed = 1.0f;

    /// <summary>
    /// The color the text component was initially.
    /// </summary>
    private Color originalColor;
    /// <summary>
    /// The time since the object was activated, in Seconds.
    /// </summary>
    private float fadeOutTimer = 0.0f;
    /// <summary>
    /// A reference to the local Text component.
    /// </summary>
    private Text failedToPauseText;

    void Awake()
    {
        failedToPauseText = this.GetComponent<Text>();
        originalColor = failedToPauseText.color;
    }

	void Start () {	}
	
	void Update ()
    {
        fadeOutTimer += Time.deltaTime;
        if (fadeOutTimer >= fadeOutDelay)
        {
            failedToPauseText.color = GetFadeOutColor(failedToPauseText.color, fadeOutSpeed);

            if (failedToPauseText.color.a <= 0.0f)
            {
                ResetTextState();
            }
        }
    }

    /// <summary>
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Uses the given oldColor by fading its alpha channel out at the speed set by the given parameter and returns it.
    /// </summary>
    /// <param name="oldColor">The color to fade out.</param>
    /// <param name="fadeOutSpeed">The speed at which to fade the given color our, in Alpha/Second.</param>
    /// <returns>A more faded out version of the given oldColor.</returns>
    internal Color GetFadeOutColor(Color oldColor, float fadeOutSpeed)
    {
        Color returnColor = new Color(oldColor.r, oldColor.g, oldColor.b, oldColor.a);
        returnColor.a -= Mathf.Clamp01(returnColor.a - fadeOutSpeed * Time.deltaTime);
        return returnColor;
    }

    /// <summary>
    /// Resets the state of the text object to before it was activated.
    /// </summary>
    private void ResetTextState()
    {
        failedToPauseText.color = originalColor;
        fadeOutTimer = 0.0f;

        this.gameObject.SetActive(false);
    }
}
