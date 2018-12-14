using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Structs
{
    /// <summary>
    /// A temporary version of the EnemyData struct which is to be used within the Unity editor and later replaced by the EnemyData struct.
    /// </summary>
    [Serializable]
    public struct EnemyDataTemp
    {
        public float Rotation;
        public float SpawnX;
        public float SpawnY;
        public int Difficulty;
        public int ID;
        public List<ProjectileDataTemp> ProjectileList;
        public string Name;
    }

    public struct EnemyData
    {
        /// <summary>
        /// The rotation of the enemy to spawn on the Z axis, in degrees.
        /// </summary>
        public float Rotation
        {
            get;
            private set;
        }
        /// <summary>
        /// The X position in the level at which to spawn the enemy, in Units.
        /// </summary>
        public float SpawnX
        {
            get;
            private set;
        }
        /// <summary>
        /// The Y position in the level at which to spawn the enemy, in Units.
        /// </summary>
        public float SpawnY
        {
            get;
            private set;
        }
        /// <summary>
        /// A number indicating the difficulty of the enemy.
        /// </summary>
        public int Difficulty
        {
            get;
            private set;
        }
        /// <summary>
        /// The unique ID belonging to the enemy.
        /// </summary>
        public int ID
        {
            get;
            private set;
        }
        /// <summary>
        /// A ReadOnlyCollection used to store the list of projectile data, or null if there is no list.
        /// </summary>
        public ReadOnlyCollection<ProjectileData> ProjectileList
        {
            get
            {
                if (projectileList != null && projectileList.Count > 0)
                {
                    return projectileList.AsReadOnly();
                }

                return null;
            }
        }
        /// <summary>
        /// The name of the enemy.
        /// </summary>
        public string Name
        {
            get;
            private set;
        }

        /// <summary>
        /// A list of ProjectileData structs belonging to the enemy.
        /// </summary>
        private List<ProjectileData> projectileList;

        public EnemyData(float _rotation, float _spawnX, float _spawnY, int _difficulty, int _id, List<ProjectileData> _projectileList, string _name)
        {
            Rotation = _rotation;
            SpawnX = _spawnX;
            SpawnY = _spawnY;
            Difficulty = _difficulty;
            ID = _id;
            projectileList = _projectileList;
            Name = _name;
        }
    }
}
