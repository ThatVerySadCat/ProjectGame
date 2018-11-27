using UnityEngine;
using UnityEngine.UI;

using System;
using System.Collections.Generic;

public class Controller_Player_Health_Bar : MonoBehaviour 
{
    /// <summary>
    /// A reference to the players script component.
    /// </summary>
    private Controller_Player player;
    /// <summary>
    /// A reference to the foreground childs Image component.
    /// </summary>
    private Image foregroundImage;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller_Player>();
        foregroundImage = this.transform.GetChild(1).GetComponent<Image>();
    }

	void Start () {	}

    void Update() { }

	void LateUpdate ()
    {
        float healthPercentage = Mathf.Clamp01((float)player.CurrentHealth / (float)player.MaxHealth);
        foregroundImage.fillAmount = healthPercentage;
    }
}
