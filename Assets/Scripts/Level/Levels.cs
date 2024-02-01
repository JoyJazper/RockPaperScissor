using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Levels", menuName = "Level/CreateLevelList")]
public class Levels : ScriptableObject
{
    /// <summary>
    /// Used for versioning of levels by level designer.
    /// </summary>
#pragma warning disable CS0414
    [SerializeField] private string version = "0.0.1";
#pragma warning restore CS0414
    public List<LevelData> levelsInGame = new List<LevelData>();
}