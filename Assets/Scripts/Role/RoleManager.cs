using UnityEngine.PlayerLoop;
using RPS.Enums;
using RPS;
using System.Collections.Generic;
using System;
namespace RPS.Game
{
    internal class RoleManager
    {
        public static RoleManager instance;

        public void Init()
        {
            if (instance != null)
            {
                instance.Destroy();
                instance = null;
            }
            instance = this;
        }

        private RoleType playerSelection = RoleType.None;
        private RoleType enemySelection = RoleType.None;

        private List<RoleType> playerRoles = new List<RoleType>();
        private List<RoleType> enemyRoles = new List<RoleType>();

        public RoleManager (List<RoleType> playerR, List<RoleType> enemyR)
        {
            Init();
            playerRoles = playerR;
            enemyRoles = enemyR;
        }

        public bool HasCard()
        {
            if(playerRoles.Count > 0 && enemyRoles.Count > 0) 
            {
                return true;
            }
            return false;
        }

        public bool SelectPlayerRole (RoleType playerRole) 
        {
            if (playerRoles.Count != 0 && playerRoles.Contains (playerRole))
            {
                playerSelection = playerRole;
                playerRoles.Remove (playerRole);
                return true;
            }
            return false;
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
            if(playerSelection == RoleType.None)
            {
                playerSelection = GameUtility.SelectRandomRole(playerRoles);
                playerRoles.Remove(playerSelection);
            }
            if(enemySelection == RoleType.None)
            {
                SelectEnemyRole();
            }

            ActionMap currentAction = GameUtility.Instance.GetAction(playerSelection, enemySelection);
            playerSelection = RoleType.None;
            enemySelection = RoleType.None;
            return currentAction;
        }

        public void Destroy()
        {
            playerRoles.Clear();
            enemyRoles.Clear();
            playerSelection = RoleType.None;
            enemySelection = RoleType.None;
            playerRoles = null;
            enemyRoles = null;
            instance = null;
        }
    }
}