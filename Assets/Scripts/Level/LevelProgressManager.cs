using UnityEngine;
using RPS.Constants;
using RPS.Systems;
using RPS.Models;
using System;
using UnityEngine.UIElements;
using UnityEditor;

namespace RPS
{
    internal class LevelProgressManager : Singleton<LevelProgressManager>, IProgressManager
    {
        int currentLevel = 0;
        float levelProgress = 0f;
        public int CurrentLevel { get { return currentLevel; } }
        public float LevelProgress { get { return levelProgress; } }

        protected override void Awake()
        {
            LoadData();
            base.Awake();
        }

        private void Start()
        {
        }

        private void LoadData()
        {
            if (PlayerPrefs.HasKey(GameConstants.LEVEL))
            {
                currentLevel = PlayerPrefs.GetInt(GameConstants.LEVEL);
            }

            if (PlayerPrefs.HasKey(GameConstants.LEVELPROGRESS))
            {
                levelProgress = PlayerPrefs.GetFloat(GameConstants.LEVELPROGRESS);
            }
            GameData.level.Value = currentLevel;
            GameData.currentProgress.Value = levelProgress;
            
            SaveData();
        }

        public void SaveProgress(float progress)
        {
            levelProgress = progress;
        }

        public void SaveLevel(int level)
        {
            currentLevel = level;
        }

        private void SaveData()
        {
            PlayerPrefs.SetInt(GameConstants.LEVEL, currentLevel);
            PlayerPrefs.SetFloat(GameConstants.LEVELPROGRESS, levelProgress);
            PlayerPrefs.Save();
        }

        public void ResetData()
        {
            PlayerPrefs.DeleteKey(GameConstants.LEVEL);
            PlayerPrefs.DeleteKey(GameConstants.LEVELPROGRESS);
        }

        protected override void OnDestroy()
        {
            SaveData();
            base.OnDestroy();
        }
    }
}

