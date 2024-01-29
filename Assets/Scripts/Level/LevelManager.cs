using UnityEngine;
using RPS.Systems;
using UnityEngine.SceneManagement;
using RPS.Enums;
using System.Collections.Generic;

namespace RPS.Game
{
    public class LevelManager : Singleton<LevelManager>
    {
        public Levels levels;
        private LevelProgressManager progressManager;
        private GameManager gameManager;

        private void Start()
        {
            //Setup();
        }

        public void Setup()
        {
            Debug.Log("ERNOS : setup of level manager");
            progressManager = LevelProgressManager.instance;
            gameManager = RPSSystemManager.Instance.game;
            LevelID currentLevel = (LevelID)GameData.currentLevel;
            GameData.isLastLevel = (LevelID)((int)currentLevel + 1) == LevelID.none;

            foreach (var level in levels.levelsInGame)
            {
                if(level.levelID == currentLevel)
                {
                    GameData.currentLevelData = level; break;
                }
            }
            GameUtility.Instance.SetRolesInGame();
        }

        public void SaveProgress(float health)
        {
            progressManager.levelProgress = health;
            progressManager.SaveData();
        }

        bool updating;
        public void NextLevel()
        {
            if (updating) return;
            updating = true;
            progressManager.currentLevel += 1;
            progressManager.levelProgress = 0f;
            progressManager.SaveData();
            SceneManager.LoadScene(0);
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

