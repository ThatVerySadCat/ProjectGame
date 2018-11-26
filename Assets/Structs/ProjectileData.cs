using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Structs
{
    /// <summary>
    /// A temporary version of the ProjectileData struct which is to be used within the Unity editor and later replaced by the ProjectileData struct.
    /// </summary>
    [Serializable]
    public struct ProjectileDataTemp
    {
        public float RelativeMovementDirectionX;
        public float RelativeMovementDirectionY;
        public float RelativeSpawnX;
        public float RelativeSpawnY;
        public int ProjectileType;
        public List<object> ParameterList;
    }

    public struct ProjectileData
    {
        /// <summary>
        /// The movement direction, relative to the enemy, along the X axis to move into.
        /// </summary>
        public float RelativeMovementDirectionX
        {
            get;
            private set;
        }
        /// <summary>
        /// The movement direction, relative to the enemy, along the Y axis to move into.
        /// </summary>
        public float RelativeMovementDirectionY
        {
            get;
            private set;
        }
        /// <summary>
        /// The X position, relative to the enemy, at which to spawn the projectile, in Units.
        /// </summary>
        public float RelativeSpawnX
        {
            get;
            private set;
        }
        /// <summary>
        /// The Y position, relative to the enemy, at which to spawn the projectile, in Units.
        /// </summary>
        public float RelativeSpawnY
        {
            get;
            private set;
        }
        /// <summary>
        /// An integer to indicate the type of projectile.
        /// </summary>
        public int ProjectileType
        {
            get;
            private set;
        }
        /// <summary>
        /// A ReadOnlyCollection containing the list of projectile data.
        /// </summary>
        public ReadOnlyCollection<object> ParameterList
        {
            get
            {
                return parameterList.AsReadOnly();
            }
        }
        
        /// <summary>
        /// A list containing the ProjectileData belonging to the enemy.
        /// </summary>
        private List<object> parameterList;

        public ProjectileData(float _relativeMovementDirectionX, float _relativeMovementDirectionY, float _relativeSpawnX, float _relativeSpawnY, int _projectileType, List<object> _parameterList)
        {
            RelativeMovementDirectionX = _relativeMovementDirectionX;
            RelativeMovementDirectionY = _relativeMovementDirectionY;
            RelativeSpawnX = _relativeSpawnX;
            RelativeSpawnY = _relativeSpawnY;
            ProjectileType = _projectileType;
            parameterList = _parameterList;
        }
    }
}
