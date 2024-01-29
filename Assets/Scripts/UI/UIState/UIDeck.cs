using System;

public class UIDeck : IUIState
{
    public static event Action OnDeckButton;
    public void OnStateEnter()
    {
        UIReferences.Instance.deckBase.gameObject.SetActive(true);
        UIReferences.Instance.playerCardBase.gameObject.SetActive(false);
        UIReferences.Instance.enemyCardBase.gameObject.SetActive(true);
        UIReferences.Instance.deck.onClick.AddListener(()=> OnDeckButton?.Invoke());
    }

    public void OnStateExit()
    {
        UIReferences.Instance.deckBase.gameObject.SetActive(false);
        UIReferences.Instance.deck.onClick.RemoveAllListeners();
        UIReferences.Instance.enemyCardBase.gameObject.SetActive(false);
    }
}
