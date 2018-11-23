using UnityEngine;

using System;
using System.Collections.Generic;

public class Controller_Player_Camera : MonoBehaviour 
{
    [Tooltip("The value which determines how far the camera can jut out from the center with the player, in Units relative to the players position."), Range(0.0f, 1.0f)]
    public float maximumPositionDivergencePercentage = 1.0f;

    /// <summary>
    /// The bounds of the level, in Units.
    /// </summary>
    private Bounds levelBounds;
    /// <summary>
    /// A reference to the script belonging to the Player.
    /// </summary>
    private Controller_Player player;
    /// <summary>
    /// A reference to the local Transform component.
    /// </summary>
    private Transform localTransform;
    /// <summary>
    /// The maximum amount, on both axis, that the camera can diverge from the center relative to the player, in Units.
    /// </summary>
    private Vector2 maximumPositionDivergence = Vector2.zero;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller_Player>();
        localTransform = this.transform;
    }

	void Start ()
    {
        levelBounds = GameObject.FindGameObjectWithTag("Level Bounds Manager").GetComponent<Controller_Level_Bounds_Manager>().LevelBounds;
        float maximumPositionDivergenceY = levelBounds.extents.y * maximumPositionDivergencePercentage;
        float maximumPositionDivergenceX = levelBounds.extents.x * maximumPositionDivergencePercentage;
        maximumPositionDivergence = new Vector2(maximumPositionDivergenceX, maximumPositionDivergenceY);
    }
	
	void Update () { }

    void LateUpdate()
    {
        float playerToEdgeWidthPercentage = Mathf.Clamp(player.Position.x / levelBounds.extents.x, -1.0f, 1.0f);
        float playerToEdgeHeightPercentage = Mathf.Clamp(player.Position.y / levelBounds.extents.y, -1.0f, 1.0f);

        Vector3 newPosition = Vector3.zero;
        newPosition.x += playerToEdgeWidthPercentage * maximumPositionDivergence.x;
        newPosition.y += playerToEdgeHeightPercentage * maximumPositionDivergence.y;
        newPosition.z = localTransform.position.z;
        localTransform.position = newPosition;
    }
}
