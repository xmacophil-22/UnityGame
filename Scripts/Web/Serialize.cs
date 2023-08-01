using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serialize
{
    ///////////////////////////////////////////////Überschreibt Playerstats mit online Stats
    public static string seri(PlayerStats playerStats){
        string j = JsonUtility.ToJson(playerStats);
        return j;
    }

}
