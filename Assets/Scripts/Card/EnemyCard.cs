using RPS.Enums;
using RPS.Systems;
using RPS.Models;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCard : MonoBehaviour
{
    private RoleType role = RoleType.None;
    public RoleType Role { get { return role; }}

    [SerializeField] private Image playercardIcon;
    public bool canInteract = true;
    public void SetupCard(RoleType roleType)
    {
        //Debug.LogError("ERNOS : setting role");
        role = roleType;
        if (role != RoleType.None)
            playercardIcon.sprite = GameData.GetPlayerSprite(role);
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
