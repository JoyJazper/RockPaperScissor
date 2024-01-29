public class UIGame : IUIState
{
    public void OnStateEnter()
    {
        UIReferences.Instance.BoardBase.gameObject.SetActive(true);

        UIReferences.Instance.playerCardBase.gameObject.SetActive(true);
        UIReferences.Instance.enemyCardBase.gameObject.SetActive(true);
    }


    public void OnStateExit()
    {
        
        UIReferences.Instance.BoardBase.gameObject.SetActive(false);

        UIReferences.Instance.playerCardBase.gameObject.SetActive(false);
        UIReferences.Instance.enemyCardBase.gameObject.SetActive(false);

        UIReferences.Instance.playerhand.gameObject.SetActive(false);
        UIReferences.Instance.enemyhand.gameObject.SetActive(false);
    }
}
