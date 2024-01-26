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
        private RoleManager rolesManager;

        private float levelHealth = GameConstants.LEVEL1_HEALTH;

        public override void Init() 
        {
            UIManager.Instance.EnablePlayBase(EnableDeck);
            GenerateDeck();
        }

        private void GenerateDeck()
        {
            rolesManager = new RoleManager
                (
                    GameUtility.Instance.GetnRoles(GameConstants.CARD_PER_PLAYER),
                    GameUtility.Instance.GetnRoles(GameConstants.CARD_PER_PLAYER)
                );
        }

        public override void Destroy() 
        {
            rolesManager.Destroy();
            rolesManager = null;
        }

        #region Menu

        public void PlayGame() 
        {
            //Debug.LogError("ERNOS : PlayGame");
            EnableDeck();
        }

        private void EnableDeck()
        {
            //Debug.LogError("ERNOS : EnableDeck");
            UIManager.Instance.DisablePlayBase();
            UIManager.Instance.EnableDeckBase(StartGame);
        }

        #endregion

        #region Game Initialization

        public void StartGame()
        {
            //Debug.LogError("ERNOS : StartGame");
            UIManager.Instance.DisableDeckBase();
            UIManager.Instance.EnableEnemyCardBase();
            UIManager.Instance.EnablePlayerCardBase();
            StartCountDown();
        }

        private void StartCountDown() 
        {
            //Debug.LogError("ERNOS : StartCountdown");
            GameUtility.Instance.StartTimer(3f, StopCountDown);
        }

        private void StopCountDown() 
        {
            ActionMap currentAction = rolesManager.PlayHands();
            ShowResult(currentAction);
        }

        private void ShowResult(ActionMap current)
        {
            //Debug.LogError("ERNOS : ShowResult");
            if (current.canInfluence)
            {
                Debug.LogError("ERNOS : VICTORY");
                UIManager.Instance.SetPlayerVictory();
                levelHealth -= GameConstants.LEVEL1_DAMAGE;
                UIManager.Instance.SetEnemyHealth(levelHealth);
            }else if (current.actionType == actions.none) 
            {
                Debug.LogError("ERNOS : NOTHING");
                UIManager.Instance.SetNormalBG();
            }
            else
            {
                Debug.LogError("ERNOS : DEFEAT");
                UIManager.Instance.SetEnemyVictory();
            }
            StartRestartCountdown();
        }

        private void StartRestartCountdown() 
        {
            //Debug.LogError("ERNOS : RESTARTING");
            if (rolesManager.HasCard())
            {
                GenerateDeck();
                StartGame();
            }
            else
            {
                GameUtility.Instance.StartTimer(3f, PlayGame);
            }
        }

        public void stopGame() { }

        public void pauseGame() { }

        #endregion

        #region Play Hand section
        private void StartTimer() 
        {
            GameUtility.Instance.StartTimer(3f, StopTimer);
        }

        private void StopTimer() 
        {
            ShowHands();
        }

        private void ShowHands() 
        {
            
        }

        private void HideHands() { }

        #endregion
    }
}

