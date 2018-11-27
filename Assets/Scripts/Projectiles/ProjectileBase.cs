using UnityEngine;

using System;
using System.Collections.Generic;

public abstract class ProjectileBase : MonoBehaviour
{
    [SerializeField]
    protected float baseSpeed;
    [SerializeField]
    protected float baseSpeedDifficultyModifier;
    [SerializeField]
    protected int baseDamage;

    protected Bounds levelBounds;
    protected Transform localTransform;
    protected Vector2 baseMovementDirection;

    protected static Controller_Difficulty_Manager difficultyManager;

    protected abstract bool IsOutOfBounds(Bounds levelBounds, Vector3 position);
    protected abstract Vector3 UpdatePosition(float deltaTime, float speed, Vector3 direction, Vector3 oldPosition);
}
