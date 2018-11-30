using UnityEngine;

using System;
using System.Collections.Generic;

public class ProjectileAcceleration : ProjectileStandard 
{
    /// <summary>
    /// The speed at which to accelerate each frame, in Units/Second^2.
    /// </summary>
    private float accelerationSpeed = 0.0f;

    protected override void UpdateWrapper()
    {
        currentSpeed += accelerationSpeed * Time.deltaTime;

        base.UpdateWrapper();
    }
    
    /// <summary>
    /// Sets the acceleration projectiles variables using the given parameters.
    /// </summary>
    /// <param name="_accelerationSpeed">The speed at which the projectile should accelerate, in Units/Second^2.</param>
    /// <param name="_baseSpeed">The speed at which to start moving, in Units/Second.</param>
    /// <param name="_baseMovementDirection">The direction in which to move.</param>
    public void SetValues(float _accelerationSpeed, float _baseSpeed, Vector2 _baseMovementDirection)
    {
        accelerationSpeed = _accelerationSpeed;
        baseSpeed = _baseSpeed;
        baseMovementDirection = _baseMovementDirection;
    }
}
