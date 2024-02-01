using RPS.Enums;
using RPS.Systems;
using UnityEngine;
using UnityEngine.UI;
using RPS.Models;
public class PlayerCard : MonoBehaviour
{
    private RoleType role = RoleType.None;
    public RoleType Role { get { return role; } }

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
            playercardIcon.sprite = GameData.GetPlayerSprite(role);
        canInteract = true;
    }

    private void SelectPlayerCard()
    {
        if(!GameData.lockPlayerInput)
            RPSSystemManager.Instance.uiManager.ShowPlayerHand(this);
    }

    public void CardUsed()
    {
        canInteract = false;
        gameObject.SetActive(false);
    }

    public void ResetCard()
    {
        canInteract = true;
        gameObject.SetActive(true);
    }
}
