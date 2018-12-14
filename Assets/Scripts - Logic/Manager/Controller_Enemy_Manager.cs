using UnityEngine;

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using Structs;

[assembly: InternalsVisibleTo("EnemyManagerTests")]
public class Controller_Enemy_Manager : MonoBehaviour 
{
    /// <summary>
    /// The amount of difficulty levels that are available.
    /// </summary>
    public int DifficultyLevelCount
    {
        get
        {
            return enemyDataList.Count;
        }
    }

    [Tooltip("A list filled with data to be used in editor for testing purposes.")]
    public List<EnemyDataTemp> sampleList = new List<EnemyDataTemp>();

    /// <summary>
    /// A list containing more lists, ordered based on difficulty level, with them containing the enemy data.
    /// </summary>
    private List<List<EnemyData>> enemyDataList = new List<List<EnemyData>>();
    /// <summary>
    /// The Enemy DAL interface to use for database communication.
    /// </summary>
    private IEnemyDAL enemyDAL;
    
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        sampleList.Sort(SortByAscendingDifficulty);

        // Used to convert temp structs to the actual structs. Needs to be removed ASAP.
        AssembleEnemyDataList();
    }

	void Start () {	}
	
	void Update () { }

    /// <summary>
    /// Returns the EnemyData belonging to the enemy with the given enemyID. Returns an EnemyData with ID -1 if the correct can't be found.
    /// </summary>
    /// <param name="enemyID">The ID of the enemy to find.</param>
    /// <returns>An EnemyData struct containing the necesary spawn information. Returns an EnemyData struct with ID -1 if the correct one can't be foun.</returns>
    public EnemyData GetEnemyByID(int enemyID)
    {
        foreach (List<EnemyData> enemyDataSubList in enemyDataList)
        {
            EnemyData tempData = enemyDataSubList.Find(x => x.ID == enemyID);
            if (tempData.ID == enemyID)
            {
                return tempData;
            }
        }

        return new EnemyData(0.0f, 0.0f, 0.0f, 0, -1, new List<ProjectileData>(), "");
    }

    /// <summary>
    /// Returns a random EnemyData struct with the given difficulty level.
    /// </summary>
    /// <param name="difficulty">The difficulty level to search on.</param>
    /// <returns>A random EnemyData struct with the given difficulty level.</returns>
    public EnemyData GetRandomEnemyData(int difficulty)
    {
        int randomIndex = UnityEngine.Random.Range(0, enemyDataList[difficulty].Count);
        return enemyDataList[difficulty][randomIndex];
    }

    /// <summary>
    /// This function has been made internal for testing purposes but should be treated as a private function.
    /// </summary>
    internal void AssembleEnemyDataList()
    {
        foreach (EnemyDataTemp tempData in sampleList)
        {
            List<ProjectileData> projectileDataList = new List<ProjectileData>(tempData.ProjectileList.Count);
            foreach (ProjectileDataTemp tempProjData in tempData.ProjectileList)
            {
                projectileDataList.Add(new ProjectileData(tempProjData.RelativeMovementDirectionX, tempProjData.RelativeMovementDirectionY, tempProjData.RelativeSpawnX, tempProjData.RelativeSpawnY, tempProjData.ProjectileType, tempProjData.ParameterList));
            }

            int difficulty = tempData.Difficulty;

            if (enemyDataList.Count - 1 < difficulty)
            {
                enemyDataList.Add(new List<EnemyData>());
            }

            enemyDataList[difficulty].Add(new EnemyData(tempData.Rotation, tempData.SpawnX, tempData.SpawnY, tempData.Difficulty, tempData.ID, projectileDataList, tempData.Name));
        }
    }

    /// <summary>
    /// Returns the lower value.
    /// </summary>
    /// <param name="data1"></param>
    /// <param name="data2"></param>
    /// <returns></returns>
    private int SortByAscendingDifficulty(EnemyDataTemp data1, EnemyDataTemp data2)
    {
        return data1.Difficulty.CompareTo(data2.Difficulty);
    }
}
