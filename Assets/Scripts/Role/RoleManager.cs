using RPS.Enums;
using RPS;
using System.Collections.Generic;
using System;
namespace RPS.Game
{
    internal class RoleManager
    {
        public static RoleManager Instance { get; private set; }

        private PlayerCard playerSelection;
        private RoleType enemySelection = RoleType.None;

        private List<RoleType> playerRoles = new List<RoleType>();
        private List<RoleType> enemyRoles = new List<RoleType>();

        
        public RoleManager (List<RoleType> playerR, List<RoleType> enemyR)
        {
            if(Instance != null && Instance != this)
            {
                Instance.Destroy();
            }
            Instance = this;
            //UnityEngine.Debug.LogError("ERNOS : Count : " + playerR.Count + " and " + enemyR.Count);
            playerRoles = playerR;
            enemyRoles = enemyR;
            UIManager.playerCardClicked += SelectPlayerRole;
        }

        public bool HasCard()
        {
            if(playerRoles.Count > 0 && enemyRoles.Count > 0) 
            {
                return true;
            }
            return false;
        }
        private bool lockPlayerInput =  false;
        public void SelectPlayerRole (PlayerCard playerRole) 
        {
            if (!lockPlayerInput)
                {
                    if (playerRoles.Count != 0 && playerRoles.Contains(playerRole.Role))
                    {
                        playerSelection = playerRole;
                    }
                }
        }

        public void LockPlayerRole () 
        {
            if (playerRoles.Count != 0 && playerRoles.Contains(playerSelection.Role))
            {
                playerSelection.CardUsed();
                playerRoles.Remove(playerSelection.Role);
            }
        }

        public bool SelectEnemyRole()
        {
            if(enemyRoles.Count != 0)
            {
                enemySelection = enemyRoles[0];
                enemyRoles.Remove(enemySelection);
                return true;
            }
            return false; 
        }

        public ActionMap PlayHands ()
        {
            lockPlayerInput = true;
            if (playerSelection == null)
            {
                playerSelection = UIManager.Instance.SelectRandomPlayerCard();
            }
            LockPlayerRole();
            if(enemySelection == RoleType.None)
            {
                SelectEnemyRole();
            }
            ShowHands();
            ActionMap currentAction = GameUtility.Instance.GetAction(playerSelection.Role, enemySelection);
            playerSelection = null;
            enemySelection = RoleType.None;
            lockPlayerInput = false;
            return currentAction;
        }

        public void ShowHands()
        {
            UIManager.Instance.ShowHands(playerSelection.Role, enemySelection);
        }

        public void Destroy()
        {
            UIManager.playerCardClicked -= SelectPlayerRole;
            if(playerRoles != null )
                playerRoles.Clear();
            if(enemyRoles != null )
                enemyRoles.Clear();
            playerSelection = null;
            enemySelection = RoleType.None;
            playerRoles = null;
            enemyRoles = null;
            lockPlayerInput = false;
        }
    }
}