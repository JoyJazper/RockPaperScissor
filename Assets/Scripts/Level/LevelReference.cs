using UnityEngine;

public class LevelReference : Singleton<LevelReference>
{
    [SerializeField] internal Levels levels;    
}
