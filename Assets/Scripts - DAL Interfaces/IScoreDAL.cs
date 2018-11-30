using UnityEngine;

using System;
using System.Collections.Generic;

public interface IScoreDAL 
{
    bool SendScore(int scoreToSend);
}
