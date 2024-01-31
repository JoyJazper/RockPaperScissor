using System.Collections.Generic;
using RPS.Enums;
using RPS.Constants;
using RPS.Systems;

namespace RPS.Game
{
    public class GameManager : IRPSSystem
    {
        private UIManager uiManager;
        private RoleManager roleManager;
        public void Init() 
        {
        }

        public void Start()
        {
            uiManager = RPSSystemManager.Instance.uiManager;
        }

        #region Menu

        public void MenuStarted()
        {
            LevelManager.Instance.Setup();
        }

        public void PlaceDeck() 
        {
            
            AudioManager.Instance.PlayBGMFX(AudioClipID.DeckBG);
            AudioManager.Instance.PlaySFX(AudioClipID.PlaySelect);
            EnableDeckbase();
        }

        private void EnableDeckbase()
        {
            GenerateDeck();
            uiManager.EnableInstruction(GameConstants.GET_CARD);
        }

        private void GenerateDeck()
        {
            foreach (Role role in GameData.currentLevelData.rolesInGame)
            {
                GameData.roleSprites.Add(role.role, role.roleSymbol);
                GameData.rolesInGameMap.Add(role.role, CreateActionMapForRole(role));
            }
            List<RoleType> playerRoles = GameData.GetRandomRoles(GameConstants.CARD_PER_PLAYER);
            List<RoleType> enemyRoles = GameData.GetRandomRoles(GameConstants.CARD_PER_PLAYER);
            roleManager = new RoleManager
                (
                    playerRoles,
                    enemyRoles
                );
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

        #endregion

        #region Game Initialization

        public void StartGame()
        {
            //Debug.LogError("ERNOS : StartGame");
            AudioManager.Instance.StopBGMFX();
            AudioManager.Instance.PlaySFX(AudioClipID.DeckSelect);
            uiManager.DisableInstruction();
            StartCountDown();
        }

        private void StartCountDown() 
        {
            uiManager.DisableInstruction();
            if (CheckLevelFinished()) return;
            uiManager.SetNormalBG();
            uiManager.HideHands();
            uiManager.EnableCountdown(GameData.currentLevelData.DRAW_WAIT_TIME);
            uiManager.EnableInstruction(GameConstants.PLAY_HAND);
            //Debug.LogError("ERNOS : StartCountdown");
            GameUtility.Instance.DelayFor(GameData.currentLevelData.DRAW_WAIT_TIME, StopCountDown);
            AudioManager.Instance.PlaySFX(AudioClipID.HandPlayStart);
        }

        private void StopCountDown() 
        {
            //Debug.LogError("ERNOS : StopCountdown");
            uiManager.DisableInstruction();
            ActionMap currentAction = RoleManager.Instance.PlayHands();
            ShowResult(currentAction);
        }

        private void ShowResult(ActionMap current)
        {
            if (current.canInfluence)
            {
                uiManager.SetPlayerVictory(current.actionType.ToString());
                GameData.currentProgress += GameData.currentLevelData.LEVEL_DAMAGE;
                
                uiManager.SetProgression();
                AudioManager.Instance.PlaySFX(AudioClipID.Blast);
            }
            else if (current.actionType == actions.none) 
            {
                uiManager.SetNormalBG();
            }
            else
            {
                uiManager.SetEnemyVictory(current.actionType.ToString());
                if (GameData.currentProgress > 0)
                    GameData.currentProgress -= GameData.currentLevelData.LEVEL_RECOVERY;
                if(GameData.currentProgress < 0)
                    GameData.currentProgress = 0;
                uiManager.SetProgression();
                AudioManager.Instance.PlaySFX(AudioClipID.Blast);
            }
            StartRestartCountdown();
        }

        private bool CheckLevelFinished()
        {
            SaveProgress();
            if (GameData.currentProgress >= GameConstants.LEVEL_MAXPROGRESS) 
            {
                LevelUp();
                return true;
            }
            return false;
        }

        private void StartRestartCountdown() 
        {
            //Debug.LogError("ERNOS : RESTARTING");
            if (RoleManager.Instance.HasCard())
            {
                //Debug.LogError("Continuing the game");
                GameUtility.Instance.DelayFor(GameConstants.RESULT_TIME, StartCountDown);
            }
            else
            {
                //Debug.LogError("restarting the game");
                GameUtility.Instance.DelayFor(GameConstants.RESULT_TIME, uiManager.PlayDeck);
            }
        }
        
        private void SaveProgress()
        {
            LevelManager.Instance.SaveProgress(GameData.currentProgress);
        }

        private void LevelUp()
        {
            AudioManager.Instance.PlaySFX(AudioClipID.LevelUnlock);
            uiManager.PlayEnd();
        }

        public void GoToNextLevel()
        {
            LevelManager.Instance.NextLevel();
        }

        #endregion

        public void Destroy()
        {
            GameData.ClearData();
            if (RoleManager.Instance != null)
                RoleManager.Instance.Destroy();
        }
    }
}

