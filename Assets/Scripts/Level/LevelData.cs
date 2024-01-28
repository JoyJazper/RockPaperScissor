using RPS;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Level", menuName = "Level/CreateLevel")]
public class LevelData : ScriptableObject
{
    public LevelID levelID = LevelID.Level1;
    public float LEVEL_DAMAGE = 10f;
    public float LEVEL_RECOVERY = 0f;
    public float DRAW_WAIT_TIME = 6f;
    public List<Role> rolesInGame = new List<Role>();
}


