using UnityEngine;

using System;
using System.Collections.Generic;

public class Controller_Level_Bounds_Manager : MonoBehaviour 
{
    /// <summary>
    /// The bounds of the level, in Units.
    /// </summary>
    public Bounds LevelBounds
    {
        get;
        private set;
    }

    /// <summary>
    /// The height of the level, in Units.
    /// </summary>
    public float levelHeight = 0.0f;
    
    void Awake()
    {
        float levelWidth = levelHeight * (16.0f / 9.0f);
        LevelBounds = new Bounds(Vector3.zero, new Vector2(levelWidth, levelHeight));
    }
}
