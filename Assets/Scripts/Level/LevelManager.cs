using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPS;
using RPS.Systems;
using UnityEngine.SceneManagement;

namespace RPS.Game
{
    public class LevelManager : Singleton<LevelManager>
    {
        public Levels levels;
        public LevelData currentLevelData = null;

        private LevelProgressManager progressManager;
        private GameManager gameManager;
        public void Setup()
        {
            progressManager = LevelProgressManager.instance;
            gameManager = RPSSystemManager.Instance.game;
            LevelID currentLevel = (LevelID)progressManager.currentLevel;
            foreach (var level in levels.levelsInGame)
            {
                if(level.levelID == currentLevel)
                {
                    currentLevelData = level; break;
                }
            }
            GameUtility.Instance.SetRolesInGame(currentLevelData.rolesInGame, NotifyGameManager);
        }

        public void NotifyGameManager()
        {
            if(currentLevelData != null)
            {
                gameManager.SetupLevel(progressManager.levelHealth);
            }
            else
            {
                Debug.LogError("ERNOS : Current LevelData is null.");
            }
        }

        public void SaveHealth(float health)
        {
            progressManager.levelHealth = health;
            progressManager.SaveData();
        }

        public void NextLevel()
        {
            progressManager.currentLevel++;
            progressManager.levelHealth = 100;
            progressManager.SaveData();
            SceneManager.LoadScene(0);
        }

        private void Start()
        {
            
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

