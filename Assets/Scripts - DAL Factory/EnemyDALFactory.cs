using UnityEngine;

using System;
using System.Collections.Generic;

public class EnemyDALFactory 
{
    public static IEnemyDAL CreateEnemyDALInterface()
    {
        return new EnemyDAL();
    }
}
