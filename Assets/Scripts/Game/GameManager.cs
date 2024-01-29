using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPS.Enums;
using RPS.Constants;
using System;

namespace RPS.Game
{
    public class GameManager : RPSSystem
    {
        private float levelHealth;

        public override void Init() 
        {
            UIManager.Instance.EnablePlayBase();
            LevelManager.Instance.Setup();
        }

        public void SetupLevel(float Health)
        {
            UIManager.Instance.SetupUI(PlayGame, StartGame);
            levelHealth = Health;
            UIManager.Instance.SetEnemyHealth(levelHealth);
        }

        private void GenerateDeck()
        {
            List<RoleType> playerRole = GameUtility.Instance.GetnRoles(GameConstants.CARD_PER_PLAYER);
            List<RoleType> enemyRoles = GameUtility.Instance.GetnRoles(GameConstants.CARD_PER_PLAYER);
            SetupPlayerCards(playerRole);
            SetupEnemyCards(enemyRoles);
            RoleManager rolesManager = new RoleManager
                (
                    playerRole,
                    enemyRoles
                );
        }

        private void SetupPlayerCards(List<RoleType> roles)
        {
            UIManager.Instance.SetupPlayerCards(roles);
        }

        private void SetupEnemyCards(List<RoleType> roles)
        {
            UIManager.Instance.SetupEnemyCards(roles);
        }

        #region Menu

        public void PlayGame() 
        {
            //Debug.LogError("ERNOS : PlayGame");
            AudioManager.Instance.PlayBGMFX(AudioClipID.DeckBG);
            AudioManager.Instance.PlaySFX(AudioClipID.PlaySelect);
            EnableDeckbase();
        }

        private void EnableDeckbase()
        {
            GenerateDeck();
            UIManager.Instance.EnableInstruction(GameConstants.GET_CARD);
            //Debug.LogError("ERNOS : EnableDeck");
            UIManager.Instance.DisablePlayBase();
            UIManager.Instance.DisableBoardBase();
            UIManager.Instance.EnableDeckBase();
            UIManager.Instance.DisablePlayerCardBase();
        }

        #endregion

        #region Game Initialization

        public void StartGame()
        {
            //Debug.LogError("ERNOS : StartGame");
            AudioManager.Instance.StopBGMFX();
            AudioManager.Instance.PlaySFX(AudioClipID.DeckSelect);
            UIManager.Instance.DisableLevelupBase();
            UIManager.Instance.DisableDeckBase();
            UIManager.Instance.DisableInstruction();
            UIManager.Instance.EnableBoardBase();
            UIManager.Instance.EnableEnemyCardBase();
            UIManager.Instance.EnablePlayerCardBase();
            StartCountDown();
        }

        private void StartCountDown() 
        {
            UIManager.Instance.SetNormalBG();
            UIManager.Instance.HideHands();
            UIManager.Instance.EnableCountdown(LevelManager.Instance.currentLevelData.DRAW_WAIT_TIME);
            UIManager.Instance.EnableInstruction(GameConstants.PLAY_HAND);
            //Debug.LogError("ERNOS : StartCountdown");
            GameUtility.Instance.StartTimer(LevelManager.Instance.currentLevelData.DRAW_WAIT_TIME, StopCountDown);
            AudioManager.Instance.PlaySFX(AudioClipID.HandPlayStart);
        }

        private void StopCountDown() 
        {
            //Debug.LogError("ERNOS : StopCountdown");
            UIManager.Instance.DisableInstruction();
            ActionMap currentAction = RoleManager.Instance.PlayHands();
            ShowResult(currentAction);
        }

        private void ShowResult(ActionMap current)
        {
            //Debug.LogError("ERNOS : ShowResult");
            if (current.canInfluence)
            {
                //Debug.LogError("ERNOS : VICTORY");
                UIManager.Instance.SetPlayerVictory();
                SaveHealth();
                if (levelHealth > 0)
                {
                    levelHealth -= LevelManager.Instance.currentLevelData.LEVEL_DAMAGE;
                    
                }
                else
                {
                    LevelUp();
                    return;
                }
                UIManager.Instance.SetEnemyHealth(levelHealth);
                AudioManager.Instance.PlaySFX(AudioClipID.Blast);
            }else if (current.actionType == actions.none) 
            {
                //Debug.LogError("ERNOS : NOTHING");
                UIManager.Instance.SetNormalBG();
            }
            else
            {
                //Debug.LogError("ERNOS : DEFEAT");
                UIManager.Instance.SetEnemyVictory();
                if (levelHealth < 100)
                    levelHealth += LevelManager.Instance.currentLevelData.LEVEL_RECOVERY;
                UIManager.Instance.SetEnemyHealth(levelHealth);
                AudioManager.Instance.PlaySFX(AudioClipID.Blast);
            }
            StartRestartCountdown();
        }

        private void StartRestartCountdown() 
        {
            //Debug.LogError("ERNOS : RESTARTING");
            if (RoleManager.Instance.HasCard())
            {
                //Debug.LogError("Continuing the game");
                GameUtility.Instance.StartTimer(GameConstants.RESULT_TIME, StartCountDown);
            }
            else
            {
                //Debug.LogError("restarting the game");
                GameUtility.Instance.StartTimer(GameConstants.RESULT_TIME, PlayGame);
            }
        }
        
        private void SaveHealth()
        {
            LevelManager.Instance.SaveHealth(levelHealth);
        }

        private void LevelUp()
        {
            if(LevelManager.Instance.currentLevelData.levelID + 1 != LevelID.none)
            {
                UIManager.Instance.EnableLevelupBase(GoToNextLevel);
            }
            else
            {
                UIManager.Instance.EnableLevelupBase();
            }
            UIManager.Instance.DisableEnemyCardBase();
            UIManager.Instance.DisablePlayerCardBase();
            UIManager.Instance.DisableBoardBase();
            UIManager.Instance.DisableDeckBase();
            AudioManager.Instance.PlaySFX(AudioClipID.LevelUnlock);
        }

        public void GoToNextLevel()
        {
            UIManager.Instance.DisableLevelupBase();
            LevelManager.Instance.NextLevel();
        }

        #endregion

        public override void Destroy()
        {
            if (RoleManager.Instance != null)
                RoleManager.Instance.Destroy();
        }
    }
}

