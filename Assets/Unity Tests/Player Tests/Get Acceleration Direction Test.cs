using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

using System;
using System.Collections;

public class GetAccelerationDirectionTest 
{
    [Test]
    public void GetAccelerationDirectionPositive()
    {
        GameObject playerObj = new GameObject();
        Controller_Player player = playerObj.AddComponent<Controller_Player>();

        float accelerationDirection = player.GetAccelerationDirection(true, false);

        Assert.AreEqual(1.0f, accelerationDirection);
    }

    [Test]
    public void GetAccelerationDirectionNegative()
    {
        GameObject playerObj = new GameObject();
        Controller_Player player = playerObj.AddComponent<Controller_Player>();

        float accelerationDirection = player.GetAccelerationDirection(false, true);

        Assert.AreEqual(-1.0f, accelerationDirection);
    }

    [Test]
    public void GetAccelerationDirectionDoubleInputs()
    {
        GameObject playerObj = new GameObject();
        Controller_Player player = playerObj.AddComponent<Controller_Player>();

        float accelerationDirection = player.GetAccelerationDirection(true, true);

        Assert.AreEqual(0.0f, accelerationDirection);
    }

    [Test]
    public void GetAccelerationDirectionNoInputs()
    {
        GameObject playerObj = new GameObject();
        Controller_Player player = playerObj.AddComponent<Controller_Player>();

        float accelerationDirection = player.GetAccelerationDirection(false, false);

        Assert.AreEqual(0.0f, accelerationDirection);
    }
}
