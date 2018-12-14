using UnityEngine;

using System;
using System.Collections.Generic;

public class ScoreDALFactory 
{
    public static IScoreDAL CreateScoreDALInterface()
    {
        return new ScoreDAL();
    }
}
