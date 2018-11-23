using UnityEngine;

using System;
using System.Collections.Generic;

public class Controller_Player : MonoBehaviour
{
    public float movementSpeed = 1.0f;

    private Transform localTransform;

	void Start ()
    {
        localTransform = this.transform;
	}
	
	void Update ()
    {
        bool up = Input.GetButton("Up");
        bool down = Input.GetButton("Down");
        bool left = Input.GetButton("Left");
        bool right = Input.GetButton("Right");

        Move(up, down, left, right);
	}

    private void Move(bool up, bool down, bool left, bool right)
    {
        Vector2 movementDirection = Vector2.zero;
        if (up)
        {
            movementDirection.y += 1.0f;
        }
        if (down)
        {
            movementDirection.y -= 1.0f;
        }
        if (left)
        {
            movementDirection.x -= 1.0f;
        }
        if (right)
        {
            movementDirection.x += 1.0f;
        }

        localTransform.position += (Vector3)movementDirection * movementSpeed * Time.deltaTime;
    }
}
