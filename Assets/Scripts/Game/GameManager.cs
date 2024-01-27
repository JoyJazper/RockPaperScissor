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
        private float levelHealth = GameConstants.LEVEL1_HEALTH;

        public override void Init() 
        {
            UIManager.Instance.SetupUI(PlayGame, StartGame);
            UIManager.Instance.EnablePlayBase();
        }

        private void GenerateDeck()
        {
            List<RoleType> playerRole = GameUtility.Instance.GetnRoles(GameConstants.CARD_PER_PLAYER);
            SetupPlayerCards(playerRole);
            RoleManager rolesManager = new RoleManager
                (
                    playerRole,
                    GameUtility.Instance.GetnRoles(GameConstants.CARD_PER_PLAYER)
                );

        }

        private void SetupPlayerCards(List<RoleType> roles)
        {
            UIManager.Instance.SetupPlayerCards(roles);
        }

        public override void Destroy() 
        {
            if(RoleManager.Instance != null)
                RoleManager.Instance.Destroy();
        }

        #region Menu

        public void PlayGame() 
        {
            //Debug.LogError("ERNOS : PlayGame");
            EnableDeckbase();
        }

        private void EnableDeckbase()
        {
            GenerateDeck();
            //Debug.LogError("ERNOS : EnableDeck");
            UIManager.Instance.DisablePlayBase();
            UIManager.Instance.DisableBoardBase();
            UIManager.Instance.EnableDeckBase();
            UIManager.Instance.DisableEnemyCardBase();
            UIManager.Instance.DisablePlayerCardBase();
        }

        #endregion

        #region Game Initialization

        public void StartGame()
        {

            //Debug.LogError("ERNOS : StartGame");
            UIManager.Instance.DisableDeckBase();
            UIManager.Instance.EnableBoardBase();
            UIManager.Instance.EnableEnemyCardBase();
            UIManager.Instance.EnablePlayerCardBase();
            StartCountDown();
        }

        private void StartCountDown() 
        {
            UIManager.Instance.HideHands();
            //Debug.LogError("ERNOS : StartCountdown");
            GameUtility.Instance.StartTimer(3f, StopCountDown);
        }

        private void StopCountDown() 
        {
            //Debug.LogError("ERNOS : StopCountdown");
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
                levelHealth -= GameConstants.LEVEL1_DAMAGE;
                UIManager.Instance.SetEnemyHealth(levelHealth);
            }else if (current.actionType == actions.none) 
            {
                //Debug.LogError("ERNOS : NOTHING");
                UIManager.Instance.SetNormalBG();
            }
            else
            {
                //Debug.LogError("ERNOS : DEFEAT");
                UIManager.Instance.SetEnemyVictory();
            }
            StartRestartCountdown();
        }

        private void StartRestartCountdown() 
        {
            //Debug.LogError("ERNOS : RESTARTING");
            if (RoleManager.Instance.HasCard())
            {
                //Debug.LogError("Continuing the game");
                GameUtility.Instance.StartTimer(3f, StartCountDown);
            }
            else
            {
                //Debug.LogError("restarting the game");
                GameUtility.Instance.StartTimer(3f, PlayGame);
            }
        }

        #endregion

    }
}

