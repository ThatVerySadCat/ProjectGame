using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

using System;
using System.Collections;

public class AccelerateAxisTest
{
    [Test]
    public void AccelerateAxisPositiveInput()
    {
        GameObject playerObj = new GameObject(); // Create a GameObject and add a component because Unity requires it.
        Controller_Player player = playerObj.AddComponent<Controller_Player>();

        float accelerationSpeed = 2.0f;
        float oldMovementSpeed = 1.0f;
        float deltaTime = 1.5f;
        float expectedAxisSpeed = oldMovementSpeed + (accelerationSpeed * deltaTime);

        float actualAxisSpeed = player.AccelerateAxisSpeed(true, false, accelerationSpeed, deltaTime, Mathf.Infinity, oldMovementSpeed);

        Assert.AreEqual(expectedAxisSpeed, actualAxisSpeed);
    }

    [Test]
    public void AccelerateAxisNegativeInput()
    {
        GameObject playerObj = new GameObject(); // Create a GameObject and add a component because Unity requires it.
        Controller_Player player = playerObj.AddComponent<Controller_Player>();

        float accelerationSpeed = 2.0f;
        float oldMovementSpeed = 1.0f;
        float deltaTime = 1.5f;
        float expectedAxisSpeed = oldMovementSpeed - (accelerationSpeed * deltaTime);

        float actualAxisSpeed = player.AccelerateAxisSpeed(false, true, accelerationSpeed, deltaTime, Mathf.Infinity, oldMovementSpeed);

        Assert.AreEqual(expectedAxisSpeed, actualAxisSpeed);
    }

    [Test]
    public void AccelerateAxisNoInput()
    {
        GameObject playerObj = new GameObject(); // Create a GameObject and add a component because Unity requires it.
        Controller_Player player = playerObj.AddComponent<Controller_Player>();

        float accelerationSpeed = 2.0f;
        float oldMovementSpeed = 1.0f;
        float deltaTime = 1.5f;
        float expectedAxisSpeed = oldMovementSpeed;

        float actualAxisSpeed = player.AccelerateAxisSpeed(false, false, accelerationSpeed, deltaTime, Mathf.Infinity, oldMovementSpeed);

        Assert.AreEqual(expectedAxisSpeed, actualAxisSpeed);
    }

    [Test]
    public void AccelerateAxisDoubleInput()
    {
        GameObject playerObj = new GameObject(); // Create a GameObject and add a component because Unity requires it.
        Controller_Player player = playerObj.AddComponent<Controller_Player>();

        float accelerationSpeed = 2.0f;
        float oldMovementSpeed = 1.0f;
        float deltaTime = 1.5f;
        float expectedAxisSpeed = oldMovementSpeed;

        float actualAxisSpeed = player.AccelerateAxisSpeed(true, true, accelerationSpeed, deltaTime, Mathf.Infinity, oldMovementSpeed);

        Assert.AreEqual(expectedAxisSpeed, actualAxisSpeed);
    }
}
