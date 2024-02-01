using UnityEngine;
using RPS.Systems;
using UnityEngine.SceneManagement;
using RPS.Models;
using RPS.Enums;
using System.Collections.Generic;

namespace RPS.Game
{
    public class LevelManager : ILevelManager
    {
        private IProgressManager progressManager;
        private IGameManager gameManager;

        public void Init()
        {
           
        }

        public void Start() 
        {
            SetupLevel();
            GameData.currentProgress.OnValueChanged += SaveProgress;
        }

        private void SetupLevel()
        {
            progressManager = LevelProgressManager.Instance;
            gameManager = RPSSystemManager.Instance.game;
            LevelID currentLevel = (LevelID)GameData.level.Value;
            GameData.isLastLevel = (LevelID)(GameData.level.Value + 1) == LevelID.none;

            foreach (var level in LevelReference.Instance.levels.levelsInGame)
            {
                if (level.levelID == currentLevel)
                {
                    GameData.currentLevelData = level; break;
                }
            }
            foreach (Role role in GameData.currentLevelData.rolesInGame)
            {
                GameData.roleSprites.Add(role.role, role.roleSymbol);
                GameData.rolesInGameMap.Add(role.role, CreateActionMapForRole(role));
            }
        }
        private Dictionary<RoleType, ActionMap> CreateActionMapForRole(Role role)
        {
            Dictionary<RoleType, ActionMap> actionMap = new Dictionary<RoleType, ActionMap>();
            foreach (ActionMap map in role.actionMap)
            {
                actionMap.Add(map.key, map);
            }
            return actionMap;
        }

        private void SaveProgress(float progress)
        {
            progressManager.SaveProgress(progress);
        }

        bool updating;
        public void GoToNextLevel()
        {
            if (updating) return;
            updating = true;
            progressManager.SaveProgress(0f);
            progressManager.SaveLevel(GameData.level.Value+1);
            SceneManager.LoadScene(0);
        }

        
        public void Destroy()
        {
            GameData.currentProgress.OnValueChanged -= SaveProgress;
        }
        
    }
}

namespace RPS
{
    public enum LevelID
    {
        Level1 = 0,
        Level2 = 1,
        Level3 = 2,
        Level4 = 3,
        Level5 = 4,
        none = 5
    }
}

