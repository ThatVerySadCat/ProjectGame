using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

using System;
using System.Collections;
using System.Collections.Generic;

using Structs;

public class SpawnEnemyTest 
{
    public GameObject enemyObj;

    [Test]
    public void SpawnEnemyTestSingle()
    {
        GameObject enemySpawnManagerObj = new GameObject();
        Controller_Enemy_Spawn_Manager enemySpawnManager = enemySpawnManagerObj.AddComponent<Controller_Enemy_Spawn_Manager>();

        EnemyData enemyData = new EnemyData(0.0f, 0.0f, 0.0f, 0, 1, new List<ProjectileData>(), "");

        bool actualResult = false;
        if (enemySpawnManager.SpawnEnemy(enemyData, 0, new Vector2(0.16f, 0.16f)) != null)
        {
            actualResult = true;
        }

        Assert.AreEqual(true, actualResult);
    }
}
