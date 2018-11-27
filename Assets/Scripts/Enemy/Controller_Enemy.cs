using UnityEngine;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Structs;

public class Controller_Enemy : MonoBehaviour
{
    /*[Tooltip("A list of sample data used for testing purposes. TO BE DESTROYED ASAP!")]
    public List<ProjectileDataTemp> sampleList = new List<ProjectileDataTemp>();*/

    [SerializeField, Tooltip("The delay between being spawned and firing its projectiles, in Seconds.")]
    private float fireDelay = 1.0f;
    [SerializeField, Tooltip("The list containing the projectile objects in descending projectile type order.")]
    private List<GameObject> projectileObjList = new List<GameObject>();

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
        localTransform = this.transform;

        /*foreach (ProjectileDataTemp tempData in sampleList)
        {
            projectileDataList.Add(new ProjectileData(tempData.RelativeMovementDirectionX, tempData.RelativeMovementDirectionY, tempData.RelativeSpawnX, tempData.RelativeSpawnY, tempData.ProjectileType, tempData.ParameterList));
        }*/
    }

    void Start() { }

    void Update()
    {
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireDelay)
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
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Instantiates a standard type projectile using the given parameters.
    /// </summary>
    /// <param name="projObj">A reference to the GameObject to be used as the projectile template.</param>
    /// <param name="movementDirection">The direction in which the projectile should move.</param>
    /// <param name="spawnPos">The position at which the projectile should be spawned in the level, in Units.</param>
    internal void InstantiateStandardProjectile(GameObject projObj, Vector3 movementDirection, Vector3 spawnPos)
    {
        ProjectileStandard projectile = Instantiate(projObj, spawnPos, Quaternion.identity).GetComponent<ProjectileStandard>();
        projectile.SetValues(movementDirection);
    }

    /// <summary>
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// Spawns the correct projectile using the information supplied by the given ProjectileData struct.
    /// </summary>
    /// <param name="projData">The struct containing the information needed to create the projectile.</param>
    internal void SpawnProjectile(ProjectileData projData, Vector3 spawnerPosition)
    {
        Vector2 relativeMovementDirection = new Vector2(projData.RelativeMovementDirectionX, projData.RelativeMovementDirectionY);

        Vector3 relativeSpawnPos = new Vector3(projData.RelativeSpawnX, projData.RelativeSpawnY);
        Vector3 actualSpawnPos = spawnerPosition + relativeSpawnPos;

        int projectileType = projData.ProjectileType;
        GameObject projectileObj = projectileObjList[projectileType];

        if (projectileType == 0)
        {
            InstantiateStandardProjectile(projectileObj, relativeMovementDirection, actualSpawnPos);
        }
        else
        {
            throw new ArgumentException("Invalid Projectile Type Given: " + projectileType);
        }
    }
}
