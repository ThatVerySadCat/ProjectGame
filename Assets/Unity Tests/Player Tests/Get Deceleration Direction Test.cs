using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

using System;
using System.Collections;

public class GetDecelerationDirectionTest 
{
    [Test]
    public void GetDecelerationDirectionTestPositive()
    {
        GameObject playerObj = new GameObject();
        Controller_Player player = playerObj.AddComponent<Controller_Player>();

        float actualValue = player.GetDecelerationDirection(1.0f);

        Assert.AreEqual(-1.0f, actualValue);
    }

    [Test]
    public void GetDecelerationDirectionTestNegative()
    {
        GameObject playerObj = new GameObject();
        Controller_Player player = playerObj.AddComponent<Controller_Player>();

        float actualValue = player.GetDecelerationDirection(-1.0f);

        Assert.AreEqual(1.0f, actualValue);
    }

    [Test]
    public void GetDecelerationDirectionTestZero()
    {
        GameObject playerObj = new GameObject();
        Controller_Player player = playerObj.AddComponent<Controller_Player>();

        float actualValue = player.GetDecelerationDirection(0.0f);

        Assert.AreEqual(0.0f, actualValue);
    }
}
