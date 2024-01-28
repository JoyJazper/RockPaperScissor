using UnityEngine;
using RPS.Constants;

namespace RPS
{
    internal class LevelProgressManager : Singleton<LevelProgressManager>
    {
        internal int currentLevel = 0;
        internal float levelHealth = 100.0f;

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
            }

            if (PlayerPrefs.HasKey(GameConstants.LEVELHEALTH))
            {
                levelHealth = PlayerPrefs.GetFloat(GameConstants.LEVELHEALTH);
            }
        }

        internal void SaveData()
        {
            PlayerPrefs.SetInt(GameConstants.LEVEL, currentLevel);
            PlayerPrefs.SetFloat(GameConstants.LEVELHEALTH, levelHealth);
            PlayerPrefs.Save();
        }

        /*internal void ResetData()
        {
            PlayerPrefs.DeleteKey(GameConstants.LEVEL);
            PlayerPrefs.DeleteKey(GameConstants.LEVELHEALTH);
        }*/

        protected override void OnDestroy()
        {
            SaveData();
            base.OnDestroy();
        }
    }
}

