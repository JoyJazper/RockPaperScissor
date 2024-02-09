using RPS.Enums;
using RPS.Systems;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using RPS.Models;
using System;
public class PlayerCard : MonoBehaviour
{
    private RoleType role = RoleType.None;
    public RoleType Role { get { return role; } }

    [SerializeField] private Button button;
    [SerializeField] private Image playercardIcon;

    private bool canInteract = true;
    public bool CanInteract { get { return canInteract; } }

    public void SetupCard(RoleType roleType, Sprite image, bool IsInteractable, Action<PlayerCard> onSelect)
    {
        //Debug.LogError("ERNOS : setting role");
        role = roleType;
        playercardIcon.sprite = image;
        canInteract = IsInteractable;
        button.onClick.AddListener(()=> onSelect(this));
    }

    public void HideCard() { gameObject.SetActive(false); }
    public void ShowCard() {  gameObject.SetActive(true); }

    public void SetInteractable() { canInteract = true; }
    public void SetUninteractable() { canInteract = false; }
}
