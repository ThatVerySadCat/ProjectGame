using UnityEngine;

using System;
using System.Collections.Generic;

public class ProjectileStandard : ProjectileBase
{
    void Awake()
    {
        localTransform = this.transform;
    }

    protected override void StartWrapper()
    {
        currentSpeed = baseSpeed;
        currentMovementDirection = baseMovementDirection;

        GameObject levelBoundsManagerObj = GameObject.FindGameObjectWithTag("Level Bounds Manager");
        if (levelBoundsManagerObj != null)
        {
            levelBounds = levelBoundsManagerObj.GetComponent<Controller_Level_Bounds_Manager>().LevelBounds;
        }
    }
    void Start()
    {
        StartWrapper();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Controller_Player player = other.GetComponent<Controller_Player>();
        player.Damage(baseDamage);

        Destroy(this.gameObject);
    }

    protected override void UpdateWrapper()
    {
        Vector3 newPosition = UpdatePosition(Time.deltaTime, currentSpeed, currentMovementDirection, localTransform.position);
        if (IsOutOfBounds(levelBounds, newPosition))
        {
            Destroy(this.gameObject);
        }

        localTransform.position = newPosition;
    }
    void Update()
    {
        UpdateWrapper();
    }

    /// <summary>
    /// Sets the needed variables for the standard projectile using the given parameters.
    /// </summary>
    /// <param name="_baseMovementDirection">The direction in which the projectile must move.</param>
    public void SetValues(float _baseSpeed, Vector2 _baseMovementDirection)
    {
        baseSpeed = _baseSpeed;
        baseMovementDirection = _baseMovementDirection;
    }

    /// <summary>
    /// Is the given position outside of the given levelBounds?
    /// </summary>
    /// <param name="levelBounds">The level bounds to check within, in Units.</param>
    /// <param name="position">The position to check, in Units.</param>
    /// <returns>True if the position lies within the levelBounds, false otherwise.</returns>
    protected override bool IsOutOfBounds(Bounds levelBounds, Vector3 position)
    {
        return !levelBounds.Contains(position);
    }

    /// <summary>
    /// Returns the updated position using the given parameters.
    /// </summary>
    /// <param name="deltaTime">The time since the last Update call.</param>
    /// <param name="speed">The speed at which the projectile moves, in Units/Second.</param>
    /// <param name="direction">The direction the projectile moves in.</param>
    /// <param name="oldPosition">The current position of the projectile, in Units.</param>
    /// <returns>The new position to be at, in Units.</returns>
    protected override Vector3 UpdatePosition(float deltaTime, float speed, Vector3 direction, Vector3 oldPosition)
    {
        return oldPosition + direction * (speed * deltaTime);
    }
}
