using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("UnityTests")] // This class's internals have been made visible to UnityTests to allow its functions to be unit tested.
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
    [SerializeField, Tooltip("The speed at which the player character accelerates and decelerates, in Units/Second^2.")]
    private float accelerationSpeed = 1.0f;
    [SerializeField, Tooltip("The maximum allowed speed for both positive and negative directions, in Units/Second.")]
    private float maxMovementSpeed = 1.0f;

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

    public int CurrentHealth
    {
        get;
        private set;
    }
    public int MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    [Header("Combat")]
    [SerializeField]
    private float onHitShakeDuration = 1.0f;
    [SerializeField]
    private float onHitShakeMagnitude = 1.0f;
    [SerializeField]
    private int maxHealth = 100;

    private Controller_Player_Camera playerCamera;

	void Awake ()
    {
        localTransform = this.transform;

        CurrentHealth = maxHealth;
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Controller_Player_Camera>();
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

        currentMovementSpeed = UpdateMovementSpeed(up, down, left, right, accelerationSpeed, Time.deltaTime, maxMovementSpeed, currentMovementSpeed);
        localTransform.position = UpdatePosition(levelBounds, currentMovementSpeed, localTransform.position);
	}

    /// <summary>
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// </summary>
    public void Damage(int damageAmount)
    {
        CurrentHealth -= damageAmount;
        if(CurrentHealth <= 0)
        {
            SceneManager.LoadScene(0);
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(0));
        }
        else
        {
            playerCamera.Shake(onHitShakeDuration, onHitShakeMagnitude);
        }
    }

    /// <summary>
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Calculate and return the correct new movement speed using the given parameters.
    /// </summary>
    /// <param name="positiveMovementBtn">The movement button to lead to the increase of the movement speed.</param>
    /// <param name="negativeMovementBtn">The movement button to lead to the decrease of the movement speed.</param>
    /// <param name="acceleration">The speed at which acceleration occurs, in Units/Second^2.</param>
    /// <param name="oldMovementSpeed">The current movement speed to change, in Units/Second.</param>
    /// <param name="deltaTime">The time since the last frame update, in Seconds.</param>
    /// <returns>Returns the correct movement speed using the given parameters, in Units/Second.</returns>
    internal float AccelerateAxisSpeed(bool positiveMovementBtn, bool negativeMovementBtn, float acceleration, float deltaTime, float maxMovementSpeed, float oldMovementSpeed)
    {
        float accelerationDirection = GetAccelerationDirection(positiveMovementBtn, negativeMovementBtn);
        return Mathf.Clamp(oldMovementSpeed + accelerationDirection * acceleration * deltaTime, -maxMovementSpeed, maxMovementSpeed);
    }

    /// <summary>
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Calculate and return the new movement speed using the given parameter, ensuring deceleration to the point of 0.0f.
    /// </summary>
    /// <param name="deceleration">The speed at which to decelerate the axis speed, in Units/Second^2.</param>
    /// <param name="deltaTime">The time since the last frame update, in Seconds.</param>
    /// <param name="maxMovementSpeed">The maximum allowed movement speed, in Units/Second.</param>
    /// <param name="oldMovementSpeed">The current movement speed to change, in Units/Second.</param>
    /// <returns>Returns the correct movement speed using the given parameter, ensuring the returned value always ends up at 0.0f, in Units/Second.</returns>
    internal float DecelerateAxisSpeed(float deceleration, float deltaTime, float maxMovementSpeed, float oldMovementSpeed)
    {
        float decelerationDirection = GetDecelerationDirection(oldMovementSpeed);
        float newMovementSpeed = Mathf.Clamp(oldMovementSpeed + decelerationDirection * deceleration * deltaTime, -maxMovementSpeed, maxMovementSpeed);

        if ((oldMovementSpeed < 0.0f && newMovementSpeed > 0.0f) || (oldMovementSpeed > 0.0f && newMovementSpeed < 0.0f))
        {
            return 0.0f;
        }

        return newMovementSpeed;
    }

    /// <summary>
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Returns the correct direction to move into, using the given parameters, assuming acceleration is desired.
    /// </summary>
    /// <param name="positiveDirectionBtn">The movement button to lead to the increase of movement speed.</param>
    /// <param name="negativeDirectionBtn">The movement button to lead to the decrease of movement speed.</param>
    /// <returns>Returns either 1.0f, -1.0f or 0.0f based on the given parameters.</returns>
    internal float GetAccelerationDirection(bool positiveDirectionBtn, bool negativeDirectionBtn)
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
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Returns the correct direction to move into, using the given parameter, assuming deceleration is desired.
    /// </summary>
    /// <param name="speedValue">The current speed value, in Units/Second.</param>
    /// <returns>Returns either -1.0f, 1.0f or 0.0f based on the given parameter.</returns>
    internal float GetDecelerationDirection(float speedValue)
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
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Returns a movement speed value, on both axis, using the given parameters and according to the set rules.
    /// </summary>
    /// <param name="up">Make vertical movement speed go up?</param>
    /// <param name="down">Make vertical movement speed go down?</param>
    /// <param name="left">Make horizontal movement speed go down?</param>
    /// <param name="right">Make horizontal movement speed go up?</param>
    /// <param name="acceleration">The speed at which to accelerate, in Units/Second^2.</param>
    /// <param name="deltaTime">The time since the last frame update, in Seconds.</param>
    /// <param name="oldMovementSpeed">The current movement speed to change, in Units/Second.</param>
    /// <returns>Returns the updated movement speed using the given parameters, in Units/Second.</returns>
    internal Vector3 UpdateMovementSpeed(bool up, bool down, bool left, bool right, float acceleration, float deltaTime, float maxMovementSpeed, Vector3 oldMovementSpeed)
    {
        if (up ^ down)
        {
            oldMovementSpeed.y = AccelerateAxisSpeed(up, down, acceleration, deltaTime, maxMovementSpeed, oldMovementSpeed.y);
        }
        else if (oldMovementSpeed.y != 0.0f)
        {
            oldMovementSpeed.y = DecelerateAxisSpeed(acceleration, deltaTime, maxMovementSpeed, oldMovementSpeed.y);
        }

        if (left ^ right)
        {
            oldMovementSpeed.x = AccelerateAxisSpeed(right, left, acceleration, deltaTime, maxMovementSpeed, oldMovementSpeed.x);
        }
        else if (oldMovementSpeed.x != 0.0f)
        {
            oldMovementSpeed.x = DecelerateAxisSpeed(acceleration, deltaTime, maxMovementSpeed, oldMovementSpeed.x);
        }

        return oldMovementSpeed;
    }

    /// <summary>
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Returns a new position using the given parameters and ensuring the returned position does not fall outside of level bounds.
    /// </summary>
    /// <param name="levelBounds">The bounds of the level, in Units.</param>
    /// <param name="currentMovementSpeed">The current movement speed on the X and Y axis, in Units/Second.</param>
    /// <param name="oldPosition">The current position to update, in Units.</param>
    /// <returns>Returns a new position using the given variables and ensuring the returned position does not fall outside of level bounds, in Units.</returns>
    internal Vector3 UpdatePosition(Bounds levelBounds, Vector2 currentMovementSpeed, Vector2 oldPosition)
    {
        Vector3 newPosition = localTransform.position + (Vector3)currentMovementSpeed * Time.deltaTime;
        newPosition.x = Mathf.Clamp(newPosition.x, levelBounds.min.x, levelBounds.max.x);
        newPosition.y = Mathf.Clamp(newPosition.y, levelBounds.min.y, levelBounds.max.y);
        
        return newPosition;
    }
}