using UnityEngine;
using RPS.Constants;

namespace RPS
{
    internal class LevelProgressManager : Singleton<LevelProgressManager>
    {
        internal int currentLevel = 0;
        internal float levelProgress = 0f;

        protected override void Awake()
        {
            base.Awake();
            LoadData();
        }

        internal void LoadData()
        {
            if (PlayerPrefs.HasKey(GameConstants.LEVEL))
            {
                currentLevel = PlayerPrefs.GetInt(GameConstants.LEVEL);
                GameData.currentLevel = currentLevel;
            }

            if (PlayerPrefs.HasKey(GameConstants.LEVELPROGRESS))
            {
                levelProgress = PlayerPrefs.GetFloat(GameConstants.LEVELPROGRESS);
                GameData.currentProgress = levelProgress;
            }
        }

        internal void SaveData()
        {
            PlayerPrefs.SetInt(GameConstants.LEVEL, currentLevel);
            PlayerPrefs.SetFloat(GameConstants.LEVELPROGRESS, levelProgress);
            PlayerPrefs.Save();
        }

        internal void ResetData()
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

