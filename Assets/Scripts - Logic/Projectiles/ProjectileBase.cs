using UnityEngine;

using System;
using System.Collections.Generic;

public abstract class ProjectileBase : MonoBehaviour
{
    [SerializeField, Tooltip("The damage the projectile will do to the player on touch.")]
    protected int baseDamage;

    /// <summary>
    /// The bounds of the level, in Units.
    /// </summary>
    protected Bounds levelBounds;
    /// <summary>
    /// The given starting speed of the projectile, in Units/Second.
    /// </summary>
    protected float baseSpeed;
    /// <summary>
    /// The current speed of the projectile, in Units/Second.
    /// </summary>
    protected float currentSpeed;
    /// <summary>
    /// A reference to the local Transform component.
    /// </summary>
    protected Transform localTransform;
    /// <summary>
    /// The given direction in which the projectile should move.
    /// </summary>
    protected Vector2 baseMovementDirection;
    /// <summary>
    /// The current direction in which the projectile should move.
    /// </summary>
    protected Vector2 currentMovementDirection;

    /// <summary>
    /// A reference to the difficulty managers script component.
    /// </summary>
    protected static Controller_Difficulty_Manager difficultyManager;

    /// <summary>
    /// Is the given position outside of the given level bounds?
    /// </summary>
    /// <param name="levelBounds">The bounds of the level, in Units.</param>
    /// <param name="position">The position to check, in Units.</param>
    /// <returns>Return true if the given position is out of bounds. Returns false otherwise.</returns>
    protected abstract bool IsOutOfBounds(Bounds levelBounds, Vector3 position);
    /// <summary>
    /// Returns the correct position using the given parameters.
    /// </summary>
    /// <param name="deltaTime">The time since the last call to Update, in Seconds.</param>
    /// <param name="speed">The speed at which to move, in Units/Second.</param>
    /// <param name="direction">The direction in which to move.</param>
    /// <param name="oldPosition">The current position, in Units.</param>
    /// <returns>A new position using the given parameters, in Units.</returns>
    protected abstract Vector3 UpdatePosition(float deltaTime, float speed, Vector3 direction, Vector3 oldPosition);
    /// <summary>
    /// A wrapper to be used instead of the default Unity Start function.
    /// </summary>
    protected abstract void StartWrapper();
    /// <summary>
    /// A wrapper to be used instead of the default Unity Update function.
    /// </summary>
    protected abstract void UpdateWrapper();
}
