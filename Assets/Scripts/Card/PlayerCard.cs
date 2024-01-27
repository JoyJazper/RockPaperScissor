using RPS.Enums;
using RPS.Systems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCard : MonoBehaviour
{
    public RoleType role = RoleType.None;
    [SerializeField] private Button button;
    [SerializeField] private Image playercardIcon;
    public bool canInteract = true;

    private void Start()
    {
        button.onClick.AddListener(SelectPlayerCard);
    }

    public void SetupCard(RoleType roleType)
    {
        //Debug.LogError("ERNOS : setting role");
        role = roleType;
        if (role != RoleType.None)
            playercardIcon.sprite = GameUtility.Instance.GetPlayerSprite(role);
        canInteract = true;
    }

    private void SelectPlayerCard()
    {
        UIManager.Instance.SelectPlayerCard(this);
    }

    public void CardUsed()
    {
        canInteract = false;
        gameObject.SetActive(false);
    }
}
