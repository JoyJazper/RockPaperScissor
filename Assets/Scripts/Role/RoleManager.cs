using RPS.Enums;
using System.Collections.Generic;

namespace RPS.Game
{
    internal class RoleManager
    {
        public static RoleManager Instance { get; private set; }

        private UIReferences uIRef;

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
            uIRef = UIReferences.Instance;
            //UnityEngine.Debug.LogError("ERNOS : Count : " + playerR.Count + " and " + enemyR.Count);
            playerRoles = playerR;
            enemyRoles = enemyR;
            UIManager.playerCardClicked += SelectPlayerRole;
            SetupPlayerCards();
            SetupEnemyCards();
            EnableAllCards();
        }

        public void SetupPlayerCards()
        {
            if (playerRoles.Count == uIRef.playerCards.Count)
                for (int i = 0; i < playerRoles.Count; i++)
                {
                    uIRef.playerCards[i].SetupCard(playerRoles[i]);
                }
        }

        public void SetupEnemyCards()
        {
            if (enemyRoles.Count == uIRef.enemyCards.Count)
                for (int i = 0; i < enemyRoles.Count; i++)
                {
                    uIRef.enemyCards[i].SetupCard(enemyRoles[i]);
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
                playerSelection = SelectRandomPlayerCard();
            }
            LockPlayerRole();
            if(enemySelection == RoleType.None)
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
            foreach (PlayerCard card in UIReferences.Instance.playerCards)
            {
                if (card.canInteract)
                {
                    selection = card;
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