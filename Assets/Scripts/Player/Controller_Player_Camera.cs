using UnityEngine;

using System;
using System.Collections.Generic;

public class Controller_Player_Camera : MonoBehaviour 
{
    [Header("Movement")]
    [SerializeField, Tooltip("The value which determines how far the camera can jut out from the center with the player, in Units relative to the players position."), Range(0.0f, 1.0f)]
    private float maximumPositionOffsetPercentage = 1.0f;

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
    private Vector2 maximumPositionOffset = Vector2.zero;

    [Header("Shake")]
    [SerializeField, Tooltip("Enable the camera shake effect?")]
    private bool enableShake = true;

    /// <summary>
    /// Is the camera currently shaking?
    /// </summary>
    private bool isShaking = false;
    /// <summary>
    /// The duration of the current shake, in Seconds.
    /// </summary>
    private float shakeDuration = 0.0f;
    /// <summary>
    /// The magnitude of the current shake, in Units.
    /// </summary>
    private float shakeMagnitude = 0.0f;
    /// <summary>
    /// The time since the shake has started, in Seconds.
    /// </summary>
    private float shakeTimer = 0.0f;
    /// <summary>
    /// The offset caused by the shake, in Units.
    /// </summary>
    private Vector3 shakeOffset = Vector3.zero;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Controller_Player>();
        localTransform = this.transform;
    }

	void Start ()
    {
        levelBounds = GameObject.FindGameObjectWithTag("Level Bounds Manager").GetComponent<Controller_Level_Bounds_Manager>().LevelBounds;
        float maximumPositionDivergenceY = levelBounds.extents.y * maximumPositionOffsetPercentage;
        float maximumPositionDivergenceX = levelBounds.extents.x * maximumPositionOffsetPercentage;
        maximumPositionOffset = new Vector2(maximumPositionDivergenceX, maximumPositionDivergenceY);
    }
	
	void Update ()
    {
        if (isShaking)
        {
            if (shakeDuration > shakeTimer)
            {
                shakeTimer += Time.deltaTime;
                shakeOffset = CalculateShakeOffset(shakeDuration, shakeMagnitude, shakeTimer);
            }
            else
            {
                isShaking = false;
                shakeOffset = Vector3.zero;
            }
        }
    }

    void LateUpdate()
    {
        Vector2 playerToEdgePercentages = GetPercentagesFromBoundsEdge(levelBounds, player.Position);
        Vector3 playerPositionOffset = GetPositionWithPercentages(localTransform.position.z, playerToEdgePercentages, maximumPositionOffset);

        localTransform.position = playerPositionOffset + shakeOffset;
    }

    /// <summary>
    /// Causes the camera to shake using the given parameters, assuming shaking has been enabled.
    /// </summary>
    /// <param name="newShakeDuration">The duration of the shake, in Seconds.</param>
    /// <param name="newShakeMagnitude">The magnitude of the shake, in Units.</param>
    public void Shake(float newShakeDuration, float newShakeMagnitude)
    {
        if (enableShake)
        {
            if (!isShaking || (isShaking && (newShakeMagnitude > shakeMagnitude || (newShakeMagnitude == shakeMagnitude && newShakeDuration > (shakeDuration - shakeTimer)))))
            {
                shakeTimer = 0.0f;
                shakeDuration = newShakeDuration;
                shakeMagnitude = newShakeMagnitude;

                isShaking = true;
            }
        }
    }

    /// <summary>
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Returns the correct shake offset using the given parameters.
    /// </summary>
    /// <param name="duration">The total time the shake has to last, in Seconds.</param>
    /// <param name="shakeMagnitude">The maximum allowed values for the shake offset, in Units.</param>
    /// <param name="timePassed">The time the shake has been, so far, going on, in Seconds.</param>
    /// <returns>The correct position offset using the given parameters, in Units.</returns>
    internal Vector3 CalculateShakeOffset(float duration, float shakeMagnitude, float timePassed)
    {
        float percentageComplete = timePassed / duration;
        float damper = 1.0f - Mathf.Clamp(4.0f * percentageComplete - 3.0f, 0.0f, 1.0f);

        float x = UnityEngine.Random.value * damper * shakeMagnitude;
        float y = UnityEngine.Random.value * damper * shakeMagnitude;

        return new Vector3(x, y);
    }

    /// <summary>
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Returns a Vector2 filled with the percentages the given position is from the edges of the given levelBounds.
    /// </summary>
    /// <param name="levelBounds">The bounds of the level, in Units.</param>
    /// <param name="position">The position to check, in Units.</param>
    /// <returns>A Vector2 containing two percentages which indicates how close the position is from the edge of the bounds with -1 and 1 being the ultimate values.</returns>
    internal Vector2 GetPercentagesFromBoundsEdge(Bounds levelBounds, Vector3 position)
    {
        float edgeWidthPercentage = Mathf.Clamp(position.x / levelBounds.extents.x, -1.0f, 1.0f);
        float edgeHeightPercentage = Mathf.Clamp(position.y / levelBounds.extents.y, -1.0f, 1.0f);

        return new Vector2(edgeWidthPercentage, edgeHeightPercentage);
    }

    /// <summary>
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Returns a position, starting at Vector3.zero, containing the offset as dictated by the given parameters.
    /// </summary>
    /// <param name="zPosition">The Z position to remain constant, in Units.</param>
    /// <param name="offsetPercentage">The percentages by which the given position must be offset.</param>
    /// <param name="maximumPositionOffset">The maximum amount of offset, in Units.</param>
    /// <returns>The position as offset from the center according to the given parameters, in Units.</returns>
    internal Vector3 GetPositionWithPercentages(float zPosition, Vector2 offsetPercentage, Vector2 maximumPositionOffset)
    {
        Vector3 newPosition = new Vector3();
        newPosition.x = offsetPercentage.x * maximumPositionOffset.x;
        newPosition.y = offsetPercentage.y * maximumPositionOffset.y;
        newPosition.z = zPosition;

        Debug.Log(offsetPercentage.y + " | " + maximumPositionOffset.y);

        return newPosition;
    }
}
