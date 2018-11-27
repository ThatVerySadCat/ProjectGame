using UnityEngine;

using System;
using System.Collections.Generic;

using Structs;

public class Controller_Enemy_Spawn_Manager : MonoBehaviour 
{
    [Header("Spawning")]
    [Tooltip("A list of sample data used for testing purposes. TO BE DESTROYED ASAP!")]
    public List<EnemyDataTemp> sampleList = new List<EnemyDataTemp>();
    
    [SerializeField, Tooltip("The starting time in between spawning two enemies, in Seconds.")]
    private float startSpawnDelay = 1.0f;
    [SerializeField, Tooltip("A reference to the GameObject to be used as the enemy template.")]
    private GameObject enemyObj;
    
    private float currentSpawnDelay = 0.0f;
    /// <summary>
    /// The time since the last enemy was spawned, in Seconds.
    /// </summary>
    private float spawnTimer = 0.0f;
    /// <summary>
    /// A list containing the data required to spawn enemies.
    /// </summary>
    private List<EnemyData> enemyDataList = new List<EnemyData>();
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
    [SerializeField]
    private float spawnDelayDifficultyAcceleration = 1.0f;

    private Controller_Difficulty_Manager difficultyManager;

    void Awake()
    {
        currentSpawnDelay = startSpawnDelay;
        enemyColliderSize = enemyObj.GetComponent<BoxCollider2D>().size;
        enemyCollisionMask = (1 << 9);
        playerCollisionMask = (1 << 8);

        UnityEngine.Random.InitState((int)DateTime.Now.Ticks);

        difficultyManager = GameObject.FindGameObjectWithTag("Difficulty Manager").GetComponent<Controller_Difficulty_Manager>();

        // Used to convert temp structs to the actual structs. Needs to be removed ASAP.
        foreach(EnemyDataTemp tempData in sampleList)
        {
            List<ProjectileData> projectileDataList = new List<ProjectileData>(tempData.ProjectileList.Count);
            foreach(ProjectileDataTemp tempProjData in tempData.ProjectileList)
            {
                projectileDataList.Add(new ProjectileData(tempProjData.RelativeMovementDirectionX, tempProjData.RelativeMovementDirectionY, tempProjData.RelativeSpawnX, tempProjData.RelativeSpawnY, tempProjData.ProjectileType, tempProjData.ParameterList));
            }

            enemyDataList.Add(new EnemyData(tempData.Rotation, tempData.SpawnX, tempData.SpawnY, tempData.Difficulty, projectileDataList, tempData.Name));
        }
    }

    void Start() { }
	
	void Update ()
    {
        if (difficultyManager.ChangedThisFrame)
        {
            currentSpawnDelay = Mathf.Clamp(startSpawnDelay - difficultyManager.CurrentDifficulty * spawnDelayDifficultyAcceleration * Time.deltaTime, 0.0f, 1.0f);
        }

        spawnTimer += Time.deltaTime;
        if (spawnTimer >= currentSpawnDelay)
        {
            EnemyData randomEnemy = GetRandomEnemyData(enemyDataList);
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
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Uses the UnityEngine.Random class to return a random entry from the given enemyDataList.
    /// </summary>
    /// <param name="enemyDataList">The list from which to retrieve a random entry.</param>
    /// <returns>A random entry from the given list.</returns>
    internal EnemyData GetRandomEnemyData(List<EnemyData> enemyDataList)
    {
        int randomEnemyIndex = UnityEngine.Random.Range(0, enemyDataList.Count);
        return enemyDataList[randomEnemyIndex];
    }
}
