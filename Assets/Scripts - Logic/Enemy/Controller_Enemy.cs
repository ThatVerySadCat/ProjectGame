using UnityEngine;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Structs;

public class Controller_Enemy : MonoBehaviour
{
    /*[Tooltip("A list of sample data used for testing purposes. TO BE DESTROYED ASAP!")]
    public List<ProjectileDataTemp> sampleList = new List<ProjectileDataTemp>();*/

    [SerializeField, Tooltip("The starting delay between being spawned and firing its projectiles, in Seconds.")]
    private float startFireDelay = 1.0f;
    [SerializeField, Tooltip("The amount of time that the fire delay is reduced with when the difficulty increases, in Seconds.")]
    private float fireDelayDifficultyModifier = 1.0f;
    [SerializeField, Tooltip("The list containing the projectile objects in descending projectile type order.")]
    private List<GameObject> projectileObjList = new List<GameObject>();

    /// <summary>
    /// A reference to the difficulty managers script component.
    /// </summary>
    private Controller_Difficulty_Manager difficultyManager;
    /// <summary>
    /// The current amount of time that must be waited after spawning to fire the projectiles, in Seconds.
    /// </summary>
    private float currentFireDelay = 0.0f;
    /// <summary>
    /// The time since the enemy was spawned, in Seconds.
    /// </summary>
    private float fireTimer = 0.0f;
    /// <summary>
    /// The list containing the ProjectileData to be used.
    /// </summary>
    private List<ProjectileData> projectileDataList = new List<ProjectileData>();
    /// <summary>
    /// A reference to the local Transform component.
    /// </summary>
    private Transform localTransform;

    void Awake()
    {
        difficultyManager = GameObject.FindGameObjectWithTag("Difficulty Manager").GetComponent<Controller_Difficulty_Manager>();
        currentFireDelay = startFireDelay;
        localTransform = this.transform;

        /*foreach (ProjectileDataTemp tempData in sampleList)
        {
            projectileDataList.Add(new ProjectileData(tempData.RelativeMovementDirectionX, tempData.RelativeMovementDirectionY, tempData.RelativeSpawnX, tempData.RelativeSpawnY, tempData.ProjectileType, tempData.ParameterList));
        }*/
    }

    void Start() { }

    void Update()
    {
        currentFireDelay = GetCurrentFireDelay(difficultyManager.HasDifficultyChanged, currentFireDelay, fireDelayDifficultyModifier);

        fireTimer += Time.deltaTime;
        if (fireTimer >= currentFireDelay)
        {
            foreach (ProjectileData projData in projectileDataList)
            {
                SpawnProjectile(projData, localTransform.position);
            }

            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Sets the necesary variables for the enemy to use.
    /// </summary>
    /// <param name="_projectileDataList">The list filled with projectile data for the enemy to use.</param>
    public void SetValues(ReadOnlyCollection<ProjectileData> _projectileDataList)
    {
        projectileDataList = new List<ProjectileData>(_projectileDataList);
    }

    /// <summary>
    ///  This function has been made internal for testing purposes but should be treated as a private function.
    ///  Gets the current fire delay as in line with the given parameters.
    /// </summary>
    /// <param name="hasDifficultyChanged">Has the difficulty changed this frame?</param>
    /// <param name="currentFireDelay">The current fire delay, in Seconds.</param>
    /// <param name="fireDelayDifficultyModifier">The amount by which to lower the fire delay, in Seconds.</param>
    /// <returns>The correct fire delay, in Seconds.</returns>
    internal float GetCurrentFireDelay(bool hasDifficultyChanged, float currentFireDelay, float fireDelayDifficultyModifier)
    {
        if(hasDifficultyChanged)
        {
            return Mathf.Clamp(currentFireDelay - fireDelayDifficultyModifier, 0.0f, Mathf.Infinity);
        }

        return currentFireDelay;
    }

    /// <summary>
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Instantiates a standard type projectile using the given parameters.
    /// </summary>
    /// <param name="projObj">A reference to the GameObject to be used as the projectile template.</param>
    /// <param name="movementDirection">The direction in which the projectile should move.</param>
    /// <param name="spawnPos">The position at which the projectile should be spawned in the level, in Units.</param>
    internal void InstantiateStandardProjectile(float movementSpeed, GameObject projObj, Vector3 movementDirection, Vector3 spawnPos)
    {
        ProjectileStandard projectile = Instantiate(projObj, spawnPos, Quaternion.identity).GetComponent<ProjectileStandard>();
        projectile.SetValues(movementSpeed, movementDirection);
    }

    /// <summary>
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Instantiates an acceleration type projectile using the given parameters.
    /// </summary>
    /// <param name="accelerationSpeed">The speed at which the projectile accelerates, in Units/Second^2.</param>
    /// <param name="movementSpeed">The speed at which the projectile starts moving, in Units/Second.</param>
    /// <param name="projObj">A reference to the GameObject to be used as the projectile template.</param>
    /// <param name="movementDirection">The direction in which the projectile should move.</param>
    /// <param name="spawnPos">The position at which the projectile should be spawned in the level, in Units.</param>
    internal void InstantiateAccelerationProjectile(float accelerationSpeed, float movementSpeed, GameObject projObj, Vector3 movementDirection, Vector3 spawnPos)
    {
        ProjectileAcceleration projectile = Instantiate(projObj, spawnPos, Quaternion.identity).GetComponent<ProjectileAcceleration>();
        projectile.SetValues(accelerationSpeed, movementSpeed, movementDirection);
    }

    /// <summary>
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Spawns the correct projectile using the information supplied by the given ProjectileData struct.
    /// </summary>
    /// <param name="projData">The struct containing the information needed to create the projectile.</param>
    internal void SpawnProjectile(ProjectileData projData, Vector3 spawnerPosition)
    {
        Vector2 relativeMovementDirection = new Vector2(projData.RelativeMovementDirectionX, projData.RelativeMovementDirectionY);

        float movementSpeed = projData.ParameterList[0];

        Vector3 relativeSpawnPos = new Vector3(projData.RelativeSpawnX, projData.RelativeSpawnY);
        Vector3 actualSpawnPos = spawnerPosition + relativeSpawnPos;

        int projectileType = projData.ProjectileType;
        GameObject projectileObj = projectileObjList[projectileType];

        if (projectileType == 0)
        {
            InstantiateStandardProjectile(movementSpeed, projectileObj, relativeMovementDirection, actualSpawnPos);
        }
        else if(projectileType == 1)
        {
            float accelerationSpeed = projData.ParameterList[1];

            InstantiateAccelerationProjectile(accelerationSpeed, movementSpeed, projectileObj, relativeMovementDirection, actualSpawnPos);
        }
        else
        {
            throw new ArgumentException("Invalid Projectile Type Given: " + projectileType);
        }
    }
}
