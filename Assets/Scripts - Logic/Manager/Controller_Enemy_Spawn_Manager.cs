﻿using UnityEngine;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Structs;

[assembly: InternalsVisibleTo("EnemySpawnManagerTests")]
public class Controller_Enemy_Spawn_Manager : MonoBehaviour 
{
    [Header("Spawning")]
    [SerializeField, Tooltip("The starting time in between spawning two enemies, in Seconds.")]
    private float startSpawnDelay = 1.0f;
    [SerializeField, Tooltip("A reference to the GameObject to be used as the enemy template.")]
    private GameObject enemyObj;

    /// <summary>
    /// A reference to the enemy managers script component.
    /// </summary>
    private Controller_Enemy_Manager enemyManager;
    /// <summary>
    /// The amount of time the spawn manager has to wait between spawning two enemies, in Seconds.
    /// </summary>
    private float currentSpawnDelay = 0.0f;
    /// <summary>
    /// The time since the last enemy was spawned, in Seconds.
    /// </summary>
    private float spawnTimer = 0.0f;
    /// <summary>
    /// The collision mask that corresponds to the enemy collision layer.
    /// </summary>
    private int enemyCollisionMask;
    /// <summary>
    /// The collision mask that corresponds to the player collision layer.
    /// </summary>
    private int playerCollisionMask;
    /// <summary>
    /// The size of the enemys box collider, in Units.
    /// </summary>
    private Vector2 enemyColliderSize;

    [Header("Difficulty")]
    [SerializeField, Tooltip("The amount by which the delay between spawning two enemies decreases for each increase in difficulty, in Seconds.")]
    private float spawnDelayDifficultyAcceleration = 1.0f;

    /// <summary>
    /// A reference to the difficulty managers script component.
    /// </summary>
    private Controller_Difficulty_Manager difficultyManager;

    void Awake()
    {
        enemyManager = GameObject.FindGameObjectWithTag("Enemy Manager").GetComponent<Controller_Enemy_Manager>();
        currentSpawnDelay = startSpawnDelay;
        enemyColliderSize = enemyObj.GetComponent<BoxCollider2D>().size;
        enemyCollisionMask = (1 << 9);
        playerCollisionMask = (1 << 8);

        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);

        difficultyManager = GameObject.FindGameObjectWithTag("Difficulty Manager").GetComponent<Controller_Difficulty_Manager>();
    }

    void Start() { }
	
	void Update ()
    {
        if (difficultyManager.HasDifficultyChanged)
        {
            currentSpawnDelay = Mathf.Clamp(startSpawnDelay - spawnDelayDifficultyAcceleration, 0.0f, Mathf.Infinity);
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentSpawnDelay)
        {
            EnemyData randomEnemy = GetRandomEnemyData(Mathf.Clamp(difficultyManager.CurrentDifficulty, 0, enemyManager.DifficultyLevelCount));
            if(SpawnEnemy(randomEnemy, enemyCollisionMask ^ playerCollisionMask, enemyColliderSize))
            {
                spawnTimer = 0.0f;
            }
        }
    }

    /// <summary>
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Spawns an enemy using the given parameters. Returns true if an enemy was created, returns false otherwise.
    /// </summary>
    /// <param name="enemyData">The struct containing the data needed to spawn an enemy.</param>
    /// <param name="collisionMask">The collision mask used to check with which object(s) the enemy can't spawn on.</param>
    /// <param name="colliderSize">The size of the enemy collider to check for possible collisions, in Units.</param>
    /// <returns>True if the enemy was spawned succesfully, false otherwise.</returns>
    internal bool SpawnEnemy(EnemyData enemyData, int collisionMask, Vector2 colliderSize)
    {
        Vector3 spawnPos = new Vector3(enemyData.SpawnX, enemyData.SpawnY);
        if (!Physics2D.BoxCast(spawnPos, colliderSize, 0.0f, Vector2.zero, Mathf.Infinity, collisionMask))
        {
            Quaternion spawnRotation = Quaternion.Euler(0.0f, 0.0f, enemyData.Rotation);

            Controller_Enemy enemy = Instantiate(enemyObj, spawnPos, spawnRotation).GetComponent<Controller_Enemy>();
            enemy.SetValues(enemyData.ProjectileList);

            return true;
        }

        return false;
    }

    /// <summary>
    /// Returns a random enemy with a random difficulty, with the given parameter being the maximum.
    /// </summary>
    /// <param name="currentDifficulty">The current difficulty.</param>
    /// <returns>A random EnemyData struct from the enemy manager.</returns>
    internal EnemyData GetRandomEnemyData(int currentDifficulty)
    {
        int randomDifficulty = UnityEngine.Random.Range(0, currentDifficulty);
        return enemyManager.GetRandomEnemyData(randomDifficulty);
    }
}