using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugLevel : MonoBehaviour
{
    private void Start()
    {
        LevelSystem level = new LevelSystem();
        Debug.Log(level.GetLevelNumber());
       // level.AddExp(25);
        Debug.Log(level.GetLevelNumber());
     //   level.AddExp(12);
        Debug.Log(level.GetLevelNumber());
    }
}
