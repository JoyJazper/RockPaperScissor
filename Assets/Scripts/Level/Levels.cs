using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Levels", menuName = "Level/CreateLevelList")]
public class Levels : ScriptableObject
{
    [SerializeField] private string version = "0.0.1";
    public List<LevelData> levelsInGame = new List<LevelData>();
}