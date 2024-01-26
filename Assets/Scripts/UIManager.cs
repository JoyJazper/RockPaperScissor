using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System;
using UnityEngine.Events;
using RPS.Constants;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private void Awake()
    {
        if (Instance != null )
           Destroy( Instance );
        Instance = this;
    }

    [SerializeField] private Button play;
    [SerializeField] private Button deck;
    [SerializeField] private Button playerCard_1;
    [SerializeField] private Button playerCard_2;
    [SerializeField] private Button playerCard_3;

    [SerializeField] private Transform playBase;
    [SerializeField] private Transform deckBase;
    [SerializeField] private Transform playerCardBase;
    [SerializeField] private Transform enemyCardBase;

    [SerializeField] private Image EnemyHealth;
    [SerializeField] private Transform BG_Normal;
    [SerializeField] private Transform BG_Win;
    [SerializeField] private Transform BG_Lose;
    public void EnablePlayBase(UnityAction onPlayButton) 
    {
        play.onClick.AddListener(onPlayButton);
        playBase.gameObject.SetActive(true); 
    }
    public void EnableDeckBase(UnityAction onDeckSelect) 
    {
        deck.onClick.AddListener(onDeckSelect);
        deckBase.gameObject.SetActive(true); 
    }
    public void EnablePlayerCardBase() { playerCardBase.gameObject.SetActive(true); }
    public void EnableEnemyCardBase() { enemyCardBase.gameObject.SetActive(true); }

    public void DisablePlayBase() { playBase.gameObject.SetActive(false); }
    public void DisableDeckBase() { deckBase.gameObject.SetActive(false); }
    public void DisablePlayerCardBase() { playerCardBase.gameObject.SetActive(false); }
    public void DisableEnemyCardBase() { enemyCardBase.gameObject.SetActive(false); }

    public void SetEnemyHealth(float value)
    {
        value = GameUtility.RemapValue(value, 0, GameConstants.LEVEL1_HEALTH, 0, 1);
        if(value < 0)
        {
            EnemyHealth.fillAmount = 0;
        }else if(value > 1)
        {
            EnemyHealth.fillAmount = 1;
        }else
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

}
