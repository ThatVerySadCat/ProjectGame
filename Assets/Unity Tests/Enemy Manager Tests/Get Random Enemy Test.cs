using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;

using System;
using System.Collections;
using System.Collections.Generic;

using Structs;

public class GetRandomEnemyTest 
{
    [Test]
    public void GetRandomEnemyTestDifficulty()
    {
        GameObject enemyManagerObj = new GameObject();
        Controller_Enemy_Manager enemyManager = enemyManagerObj.AddComponent<Controller_Enemy_Manager>();

        int maxDifficulty = 5;
        List<EnemyDataTemp> sampleList = new List<EnemyDataTemp>(maxDifficulty);

        for (int i = 0; i < maxDifficulty; i++)
        {
            EnemyDataTemp enemyData = new EnemyDataTemp();
            enemyData.Difficulty = i;
            enemyData.ProjectileList = new List<ProjectileDataTemp>();
            sampleList.Add(enemyData);
        }

        enemyManager.sampleList = sampleList;
        enemyManager.AssembleEnemyDataList();

        int randomDifficulty = UnityEngine.Random.Range(0, maxDifficulty);
        EnemyData randomData = enemyManager.GetRandomEnemyData(randomDifficulty);

        Assert.AreEqual(randomDifficulty, randomData.Difficulty);
    }
}
