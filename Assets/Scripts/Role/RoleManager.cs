using RPS.Enums;
using RPS.Systems;
using RPS.Models;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RPS.Game
{
    internal class RoleManager : IRoleManager
    {
        private UIReferences uIRef;
        private PlayerCard playerSelection;
        private RoleType enemySelection = RoleType.None;
        private List<RoleType> playerRoles;
        private List<RoleType> enemyRoles;
        private bool lockPlayerInput = false;

        public RoleManager(List<RoleType> playerR, List<RoleType> enemyR)
        {
            //if (Instance != null && Instance != this)
            //{
            //    Instance.Destroy();
            //}
            //Instance = this;
            uIRef = UIReferences.Instance;
            playerRoles = playerR;
            enemyRoles = enemyR;
            UIManager.PlayerHandSelected += SelectPlayerRole;
            SetupPlayerCards();
            SetupEnemyCards();
            EnableAllCards();
        }

        public void SetupPlayerCards()
        {
            if (playerRoles.Count == uIRef.playerCards.Count)
            {
                for (int i = 0; i < playerRoles.Count; i++)
                {
                    uIRef.playerCards[i].SetupCard(playerRoles[i]);
                }
            }
        }

        public void SetupEnemyCards()
        {
            if (enemyRoles.Count == uIRef.enemyCards.Count)
            {
                for (int i = 0; i < enemyRoles.Count; i++)
                {
                    uIRef.enemyCards[i].SetupCard(enemyRoles[i]);
                }
            }
        }

        public void EnableAllCards()
        {
            foreach (PlayerCard card in uIRef.playerCards)
            {
                card.ResetCard();
            }

            foreach (EnemyCard card in uIRef.enemyCards)
            {
                card.ResetCard();
            }
        }

        public bool HasCard()
        {
            return (playerRoles.Count > 0 && enemyRoles.Count > 0);
        }

        public void SelectPlayerRole(PlayerCard playerRole)
        {
            if (!lockPlayerInput && playerRoles.Contains(playerRole.Role))
            {
                playerSelection = playerRole;
            }
        }

        public void LockPlayerRole()
        {
            if (playerRoles.Contains(playerSelection.Role))
            {
                playerSelection.CardUsed();
                playerRoles.Remove(playerSelection.Role);
            }
        }

        public bool SelectEnemyRole()
        {
            if (enemyRoles.Count > 0)
            {
                enemySelection = enemyRoles[0];
                enemyRoles.Remove(enemySelection);
                return true;
            }
            return false;
        }

        public ActionMap PlayHands()
        {
            lockPlayerInput = true;
            if (playerSelection == null)
            {
                playerSelection = SelectRandomPlayerCard();
            }
            LockPlayerRole();
            if (enemySelection == RoleType.None)
            {
                SelectEnemyRole();
            }
            ShowHands();
            ActionMap currentAction = GameData.GetAction(playerSelection.Role, enemySelection);
            playerSelection = null;
            enemySelection = RoleType.None;
            lockPlayerInput = false;
            return currentAction;
        }

        public PlayerCard SelectRandomPlayerCard()
        {
            PlayerCard selection = null;
            foreach (PlayerCard card in uIRef.playerCards)
            {
                if (card.canInteract)
                {
                    selection = card;
                    break;
                }
            }
            return selection;
        }

        public void ShowHands()
        {
            lockPlayerInput = true;
            if (playerSelection.Role != RoleType.None && enemySelection != RoleType.None)
            {
                uIRef.playerhand.sprite = GameData.GetPlayerSprite(playerSelection.Role);
                uIRef.enemyhand.sprite = GameData.GetPlayerSprite(enemySelection);
                uIRef.playerhand.gameObject.SetActive(true);
                uIRef.enemyhand.gameObject.SetActive(true);
            }
            foreach (EnemyCard card in uIRef.enemyCards)
            {
                if (card.Role == enemySelection && card.canInteract)
                {
                    card.CardUsed();
                    break;
                }
            }
        }

        public void Destroy()
        {
            UIManager.PlayerHandSelected -= SelectPlayerRole;
            playerRoles?.Clear();
            enemyRoles?.Clear();
            playerSelection = null;
            enemySelection = RoleType.None;
            playerRoles = null;
            enemyRoles = null;
            lockPlayerInput = false;
        }
    }


}