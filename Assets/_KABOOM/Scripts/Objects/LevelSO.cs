using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelSO", menuName = "SOs/LevelSO")]
public class LevelSO : ScriptableObject
{
    [Tooltip("the name of the scene associated with the level")]
    public string LevelName;
    public int KeysToWin;
    public string BGMName;
}
