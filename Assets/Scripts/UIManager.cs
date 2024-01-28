using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.Events;
using RPS.Constants;
using RPS.Enums;
using TMPro;

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
    [SerializeField] private List<EnemyCard> enemyCards;

    [SerializeField] private Transform playBase;
    [SerializeField] private Transform deckBase;
    [SerializeField] private Transform playerCardBase;
    [SerializeField] private Transform enemyCardBase;
    [SerializeField] private Transform BoardBase;
    [SerializeField] private TimerAnimation CountdownBase;
    [SerializeField] private TMP_Text InstructionText;
    [SerializeField] private BlastPlayer blast;

    [SerializeField] private Image EnemyHealth;
    [SerializeField] private BackgroundAnimation BG_Win;
    [SerializeField] private BackgroundAnimation BG_Lose;

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

    public void EnableCountdown(float targetTime)
    {
        CountdownBase.gameObject.SetActive(true);
        CountdownBase.Init(targetTime, DisableCountdown);
    }

    public void EnableInstruction(string instruction)
    {
        InstructionText.gameObject.SetActive(true);
        InstructionText.text = instruction;
    }
    public void DisableInstruction() 
    {
        InstructionText.gameObject.SetActive(false);
        InstructionText.text = "";
    }
    public void DisableCountdown() 
    {
        CountdownBase.gameObject.SetActive(false);
    }

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

    public void SetupEnemyCards(List<RoleType> roles)
    {
        if (roles.Count == enemyCards.Count)
            for (int i = 0; i < roles.Count; i++)
            {
                enemyCards[i].SetupCard(roles[i]);
            }
    }

    public void SetEnemyHealth(float value)
    {
        value = GameUtility.RemapValue(value, 0, GameConstants.LEVEL_HEALTH, 0, 1);
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
        blast.gameObject.SetActive(true);
        blast.PlayAnimation(BlastsAnimations.Blast2);
        BG_Win.DoPunch();
    }

    public void SetEnemyVictory()
    {
        blast.gameObject.SetActive(true);
        blast.PlayAnimation(BlastsAnimations.Blast3);
        BG_Lose.DoPunch();
    }

    public void SetNormalBG() 
    {
        blast.gameObject.SetActive(false);
        BG_Win.FadeOut();
        BG_Lose.FadeOut();
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
        AudioManager.Instance.PlaySFX(AudioClipID.CardSelect);
        playerhand.sprite = GameUtility.Instance.GetPlayerSprite(card.Role);
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
        foreach (EnemyCard card in enemyCards)
        {
            if(card.Role == enemy && card.canInteract)
            {
                card.CardUsed();
                break;
            }
        }
    }



    public void EnableAllCards()
    {
        foreach (PlayerCard card in playerCards)
        {
            card.ResetCard();
        }

        foreach(EnemyCard card in enemyCards)
        {
            card.ResetCard();
        }
    }

    public void HideHands()
    {
        lockPlayerInput = false;
        playerhand.gameObject.SetActive(false);
        enemyhand.gameObject.SetActive(false);
    }

}
