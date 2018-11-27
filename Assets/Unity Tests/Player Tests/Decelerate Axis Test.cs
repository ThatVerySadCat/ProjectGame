using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

using System;
using System.Collections;

public class DecelerateAxisTest
{
    [Test]
    public void DeceleratePositiveAxisSpeed()
    {
        GameObject playerObj = new GameObject(); // Create a GameObject and add a component because Unity requires it.
        Controller_Player player = playerObj.AddComponent<Controller_Player>();

        float deceleration = 1.0f;
        float deltaTime = 1.0f;
        float oldMovementSpeed = 1.0f;
        float expectedAxisSpeed = oldMovementSpeed - (deceleration * deltaTime);

        float actualAxisSpeed = player.DecelerateAxisSpeed(deceleration, deltaTime, Mathf.Infinity, oldMovementSpeed);

        Assert.AreEqual(expectedAxisSpeed, actualAxisSpeed);
    }

    [Test]
    public void DecelerateNegativeAxisSpeed()
    {
        GameObject playerObj = new GameObject(); // Create a GameObject and add a component because Unity requires it.
        Controller_Player player = playerObj.AddComponent<Controller_Player>();

        float deceleration = 1.0f;
        float deltaTime = 1.0f;
        float oldMovementSpeed = -1.0f;
        float expectedAxisSpeed = 0.0f;

        float actualAxisSpeed = player.DecelerateAxisSpeed(deceleration, deltaTime, Mathf.Infinity, oldMovementSpeed);

        Assert.AreEqual(expectedAxisSpeed, actualAxisSpeed);
    }
}
