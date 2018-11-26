using UnityEngine;

using System;
using System.Collections.Generic;

public class Controller_Player : MonoBehaviour
{
    /// <summary>
    /// The current position of the player, in Units.
    /// </summary>
    public Vector3 Position
    {
        get
        {
            return localTransform.position;
        }
    }

    [Header("Movement")]
    [Tooltip("The speed at which the player character accelerates and decelerates, in Units/Second^2.")]
    public float accelerationSpeed = 1.0f;
    [Tooltip("The maximum allowed speed for both positive and negative directions, in Units/Second.")]
    public float maxMovementSpeed = 1.0f;

    /// <summary>
    /// The bounds of the level, in Units.
    /// </summary>
    private Bounds levelBounds;
    /// <summary>
    /// A reference to the local Transform variable.
    /// </summary>
    private Transform localTransform;
    /// <summary>
    /// The current speed of the player on both axis, in Units/Second.
    /// </summary>
    private Vector2 currentMovementSpeed = Vector2.zero;

	void Awake ()
    {
        localTransform = this.transform;
	}

    void Start()
    {
        levelBounds = GameObject.FindGameObjectWithTag("Level Bounds Manager").GetComponent<Controller_Level_Bounds_Manager>().LevelBounds;
    }

	void Update ()
    {
        bool up = Input.GetButton("Up");
        bool down = Input.GetButton("Down");
        bool left = Input.GetButton("Left");
        bool right = Input.GetButton("Right");

        currentMovementSpeed = UpdateMovementSpeed(up, down, left, right, currentMovementSpeed);
        localTransform.position = UpdatePosition(levelBounds, currentMovementSpeed, localTransform.position);
	}

    /// <summary>
    /// Calculate and return the correct new movement speed using the given parameters.
    /// </summary>
    /// <param name="positiveMovementBtn">The movement button to lead to the increase of the movement speed.</param>
    /// <param name="negativeMovementBtn">The movement button to lead to the decrease of the movement speed.</param>
    /// <param name="oldMovementSpeed">The current movement speed to change, in Units/Second.</param>
    /// <returns>Returns the correct movement speed using the given parameters, in Units/Second.</returns>
    private float AccelerateAxisSpeed(bool positiveMovementBtn, bool negativeMovementBtn, float oldMovementSpeed)
    {
        float accelerationDirection = GetAccelerationDirection(positiveMovementBtn, negativeMovementBtn);
        return Mathf.Clamp(oldMovementSpeed + accelerationDirection * accelerationSpeed * Time.deltaTime, -maxMovementSpeed, maxMovementSpeed);
    }

    /// <summary>
    /// Calculate and return the new movement speed using the given parameter, ensuring deceleration to the point of 0.0f.
    /// </summary>
    /// <param name="oldMovementSpeed">The current movement speed to change, in Units/Second.</param>
    /// <returns>Returns the correct movement speed using the given parameter, ensuring the returned value always ends up at 0.0f, in Units/Second.</returns>
    private float DecelerateAxisSpeed(float oldMovementSpeed)
    {
        float decelerationDirection = GetDecelerationDirection(oldMovementSpeed);
        float newMovementSpeed = Mathf.Clamp(oldMovementSpeed + decelerationDirection * accelerationSpeed * Time.deltaTime, -maxMovementSpeed, maxMovementSpeed);

        if ((oldMovementSpeed < 0.0f && newMovementSpeed > 0.0f) || (oldMovementSpeed > 0.0f && newMovementSpeed < 0.0f))
        {
            return 0.0f;
        }

        return newMovementSpeed;
    }

    /// <summary>
    /// Returns the correct direction to move into, using the given parameters, assuming acceleration is desired.
    /// </summary>
    /// <param name="positiveDirectionBtn">The movement button to lead to the increase of movement speed.</param>
    /// <param name="negativeDirectionBtn">The movement button to lead to the decrease of movement speed.</param>
    /// <returns>Returns either 1.0f, -1.0f or 0.0f based on the given parameters.</returns>
    private float GetAccelerationDirection(bool positiveDirectionBtn, bool negativeDirectionBtn)
    {
        if (positiveDirectionBtn)
        {
            return 1.0f;
        }
        if (negativeDirectionBtn)
        {
            return -1.0f;
        }

        return 0.0f;
    }

    /// <summary>
    /// Returns the correct direction to move into, using the given parameter, assuming deceleration is desired.
    /// </summary>
    /// <param name="speedValue">The current speed value, in Units/Second.</param>
    /// <returns>Returns either -1.0f, 1.0f or 0.0f based on the given parameter.</returns>
    private float GetDecelerationDirection(float speedValue)
    {
        if (speedValue > 0.0f)
        {
            return -1.0f;
        }
        else if (speedValue < 0.0f)
        {
            return 1.0f;
        }

        return 0.0f;
    }

    /// <summary>
    /// Returns a movement speed value, on both axis, using the given parameters and according to the set rules.
    /// </summary>
    /// <param name="up">Make vertical movement speed go up?</param>
    /// <param name="down">Make vertical movement speed go down?</param>
    /// <param name="left">Make horizontal movement speed go down?</param>
    /// <param name="right">Make horizontal movement speed go up?</param>
    /// <param name="oldMovementSpeed">The current movement speed to change, in Units/Second.</param>
    /// <returns>Returns the updated movement speed using the given parameters, in Units/Second.</returns>
    private Vector3 UpdateMovementSpeed(bool up, bool down, bool left, bool right, Vector3 oldMovementSpeed)
    {
        if (up ^ down)
        {
            oldMovementSpeed.y = AccelerateAxisSpeed(up, down, oldMovementSpeed.y);
        }
        else if (oldMovementSpeed.y != 0.0f)
        {
            oldMovementSpeed.y = DecelerateAxisSpeed(oldMovementSpeed.y);
        }

        if (left ^ right)
        {
            oldMovementSpeed.x = AccelerateAxisSpeed(right, left, oldMovementSpeed.x);
        }
        else if (oldMovementSpeed.x != 0.0f)
        {
            oldMovementSpeed.x = DecelerateAxisSpeed(oldMovementSpeed.x);
        }

        return oldMovementSpeed;
    }

    /// <summary>
    /// Returns a new position using the given parameters and ensuring the returned position does not fall outside of level bounds.
    /// </summary>
    /// <param name="levelBounds">The bounds of the level, in Units.</param>
    /// <param name="currentMovementSpeed">The current movement speed on the X and Y axis, in Units/Second.</param>
    /// <param name="oldPosition">The current position to update, in Units.</param>
    /// <returns>Returns a new position using the given variables and ensuring the returned position does not fall outside of level bounds, in Units.</returns>
    private Vector3 UpdatePosition(Bounds levelBounds, Vector2 currentMovementSpeed, Vector2 oldPosition)
    {
        Vector3 newPosition = localTransform.position + (Vector3)currentMovementSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, levelBounds.min.x, levelBounds.max.x);
        newPosition.y = Mathf.Clamp(newPosition.y, levelBounds.min.y, levelBounds.max.y);
        
        return newPosition;
    }
}