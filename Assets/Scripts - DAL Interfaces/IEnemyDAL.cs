using UnityEngine;

using System;
using System.Collections.Generic;

using Structs;

public interface IEnemyDAL 
{
    List<EnemyData> GetAllEnemyData();
}
