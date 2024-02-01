using System.Collections.Generic;
using RPS.Enums;
using RPS.Constants;
using RPS.Systems;
using RPS.Models;

namespace RPS.Game
{
    public class GameManager : IGameManager
    {
        private IUIManager uiManager;
        private ILevelManager levelManager;
        private IRoleManager roleManager;
        public void Init() { }

        public void Start()
        {
            uiManager = RPSSystemManager.Instance.uiManager;
            levelManager = RPSSystemManager.Instance.levelManager;
        }

        #region Deck

        public void SetupDeck()
        {
            roleManager?.Destroy();
            List<RoleType> playerRoles = GameData.GetRandomRoles(GameConstants.CARD_PER_PLAYER);
            List<RoleType> enemyRoles = GameData.GetRandomRoles(GameConstants.CARD_PER_PLAYER);
            roleManager = new RoleManager
                (
                    playerRoles,
                    enemyRoles
                );
        }

        
        #endregion

        #region Game

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
            uiManager.ShowNormalBG();
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
            ActionMap currentAction = roleManager.PlayHands();
            ShowResult(currentAction);
        }

        private void ShowResult(ActionMap current)
        {
            uiManager.ShowHands();
            float progress = GameData.currentProgress.Value;
            if (current.canInfluence)
            {
                uiManager.ShowPlayerVictory(current.actionType.ToString());
                progress += GameData.currentLevelData.LEVEL_DAMAGE;
                AudioManager.Instance.PlaySFX(AudioClipID.Blast);
            }
            else if (current.actionType == actions.none)
            {
                uiManager.ShowNormalBG();
            }
            else
            {
                uiManager.ShowEnemyVictory(current.actionType.ToString());
                progress -= GameData.currentLevelData.LEVEL_RECOVERY;
                AudioManager.Instance.PlaySFX(AudioClipID.Blast);
            }
            GameData.currentProgress.Value = progress;
            StartResetCountdown();
        }

        private bool CheckLevelFinished()
        {
            if (GameData.currentProgress.Value >= GameConstants.LEVEL_MAXPROGRESS)
            {
                LevelUp();
                return true;
            }
            return false;
        }

        private void StartResetCountdown()
        {
            //Debug.LogError("ERNOS : RESTARTING");
            if (roleManager.HasCard())
            {
                //Debug.LogError("Continuing the game");
                GameUtility.Instance.DelayFor(GameConstants.RESULT_TIME, StartCountDown);
            }
            else
            {
                //Debug.LogError("restarting the game");
                GameUtility.Instance.DelayFor(GameConstants.RESULT_TIME, NextRound);
            }
        }

        private void NextRound()
        {
            CheckLevelFinished();
            uiManager.PlayDeck();
        }

        private void LevelUp()
        {
            AudioManager.Instance.PlaySFX(AudioClipID.LevelUnlock);
            uiManager.GameEnded();
        }

        public void IncreaseLevel()
        {
            levelManager.GoToNextLevel();
        }

        #endregion

        public void Destroy()
        {
            if (roleManager != null)
                roleManager.Destroy();
        }
    }
}

