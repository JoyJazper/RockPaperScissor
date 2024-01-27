using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.Events;
using RPS.Constants;
using RPS.Enums;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public delegate void playerCardDetail(PlayerCard role);
    public static event playerCardDetail playerCardClicked;

    private void Awake()
    {
        if (Instance != null )
           Destroy( Instance );
        Instance = this;
    }

    [SerializeField] private Button play;
    [SerializeField] private Button deck;
    [SerializeField] private List<PlayerCard> playerCards;

    [SerializeField] private Transform playBase;
    [SerializeField] private Transform deckBase;
    [SerializeField] private Transform playerCardBase;
    [SerializeField] private Transform enemyCardBase;
    [SerializeField] private Transform BoardBase;

    [SerializeField] private Image EnemyHealth;
    [SerializeField] private Transform BG_Normal;
    [SerializeField] private Transform BG_Win;
    [SerializeField] private Transform BG_Lose;

    [SerializeField] private Image playerhand;
    [SerializeField] private Image enemyhand;

    public void SetupUI(UnityAction onPlayButton, UnityAction onDeckSelect)
    {
        play.onClick.AddListener(onPlayButton);
        deck.onClick.AddListener(onDeckSelect);
    }

    public void EnablePlayBase() 
    {
        playBase.gameObject.SetActive(true); 
    }
    public void EnableDeckBase() 
    {
        deckBase.gameObject.SetActive(true); 
    }
    public void EnablePlayerCardBase() { playerCardBase.gameObject.SetActive(true); }
    public void EnableEnemyCardBase() { enemyCardBase.gameObject.SetActive(true); }

    public void EnableBoardBase() { BoardBase.gameObject.SetActive(true); }

    public void DisableBoardBase() 
    {
        BoardBase.gameObject.SetActive(false); 
        playerhand.gameObject.SetActive(false);
        enemyhand.gameObject.SetActive(false);
    }
    public void DisablePlayBase() { playBase.gameObject.SetActive(false); }
    public void DisableDeckBase() 
    {
        deckBase.gameObject.SetActive(false); 
    }
    public void DisablePlayerCardBase() { playerCardBase.gameObject.SetActive(false); }
    public void DisableEnemyCardBase() { enemyCardBase.gameObject.SetActive(false); }

    public void SetupPlayerCards(List<RoleType> roles)
    {
        EnableAllCards();
        if(roles.Count == playerCards.Count)
            for(int i = 0 ; i < roles.Count; i++) 
            {
                playerCards[i].SetupCard(roles[i]);
            }
    }
    public void SetEnemyHealth(float value)
    {
        value = GameUtility.RemapValue(value, 0, GameConstants.LEVEL1_HEALTH, 0, 1);
        if(value < 0)
        {
            EnemyHealth.fillAmount = 0;
        }
        else if(value > 1)
        {
            EnemyHealth.fillAmount = 1;
        }
        else
            EnemyHealth.fillAmount = value;
    }

    public void SetPlayerVictory()
    {
        BG_Win.gameObject.SetActive(true);
        BG_Lose.gameObject.SetActive(false);
        BG_Normal.gameObject.SetActive(false);
    }

    public void SetEnemyVictory()
    {
        BG_Win.gameObject.SetActive(false);
        BG_Lose.gameObject.SetActive(true);
        BG_Normal.gameObject.SetActive(false);
    }

    public void SetNormalBG() 
    {
        BG_Win.gameObject.SetActive(false);
        BG_Lose.gameObject.SetActive(false);
        BG_Normal.gameObject.SetActive(true);
    }

    public void RemoveSelection(Button button)
    {
        button.gameObject.SetActive(false);
    }

    public PlayerCard SelectRandomPlayerCard()
    {
        PlayerCard selection = null;
        foreach(PlayerCard card in playerCards)
        {
            if (card.canInteract)
            {
                selection = card;
            }
        }
        return selection;
    }
    
    bool lockPlayerInput = false;
    public void SelectPlayerCard(PlayerCard card)
    {
        if(lockPlayerInput) { return; }
        playerhand.sprite = GameUtility.Instance.GetPlayerSprite(card.role);
        playerhand.gameObject.SetActive(true);
        playerCardClicked.Invoke(card);
    }

    public void ShowHands(RoleType player, RoleType enemy)
    {
        lockPlayerInput = true;
        if(player != RoleType.None && enemy != RoleType.None)
        {
            playerhand.sprite = GameUtility.Instance.GetPlayerSprite(player);
            enemyhand.sprite = GameUtility.Instance.GetPlayerSprite(enemy);
            playerhand.gameObject.SetActive(true);
            enemyhand.gameObject.SetActive(true);
        }
    }

    public void EnableAllCards()
    {
        foreach (PlayerCard card in playerCards)
        {
            card.gameObject.SetActive(true);
        }
    }

    public void HideHands()
    {
        lockPlayerInput = false;
        playerhand.gameObject.SetActive(false);
        enemyhand.gameObject.SetActive(false);
    }

}
