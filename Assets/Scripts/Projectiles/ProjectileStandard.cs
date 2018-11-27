using UnityEngine;

using System;
using System.Collections.Generic;

public class ProjectileStandard : ProjectileBase
{
    void Awake()
    {
        localTransform = this.transform;
    }

    void Start()
    {
        levelBounds = GameObject.FindGameObjectWithTag("Level Bounds Manager").GetComponent<Controller_Level_Bounds_Manager>().LevelBounds;
    }

    void Update()
    {
        Vector3 newPosition = UpdatePosition(Time.deltaTime, baseSpeed, baseMovementDirection, localTransform.position);
        if (IsOutOfBounds(levelBounds, newPosition))
        {
            Destroy(this.gameObject);
        }

        localTransform.position = newPosition;
    }

    public void SetValues(Vector2 _baseMovementDirection)
    {
        baseMovementDirection = _baseMovementDirection;
    }

    protected override bool IsOutOfBounds(Bounds levelBounds, Vector3 position)
    {
        return !levelBounds.Contains(position);
    }

    protected override Vector3 UpdatePosition(float deltaTime, float speed, Vector3 direction, Vector3 oldPosition)
    {
        return oldPosition + direction * (speed * deltaTime);
    }
}
