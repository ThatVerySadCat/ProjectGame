using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Structs
{
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
        /// A ReadOnlyCollection used to store the list of projectile data.
        /// </summary>
        public ReadOnlyCollection<ProjectileData> ProjectileList
        {
            get
            {
                return projectileList.AsReadOnly();
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

        public EnemyData(float _rotation, float _spawnX, float _spawnY, int _difficulty, List<ProjectileData> _projectileList, string _name)
        {
            Rotation = _rotation;
            SpawnX = _spawnX;
            SpawnY = _spawnY;
            Difficulty = _difficulty;
            projectileList = _projectileList;
            Name = _name;
        }
    }
}
