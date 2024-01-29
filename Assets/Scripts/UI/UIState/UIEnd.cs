using RPS.Constants;
using System;

public class UIEnd : IUIState
{
    public static event Action OnContinueClick;

    public void OnStateEnter()
    {
        UIReferences.Instance.levelUpBase.gameObject.SetActive(true);
        if (!GameData.isLastLevel)
        {
            UIReferences.Instance.levelUpText.text = GameConstants.LEVEL_UP;
            UIReferences.Instance.levelUp.gameObject.SetActive(true);
            UIReferences.Instance.levelUp.onClick.AddListener(()=> OnContinueClick());
        }
        else
        {
            UIReferences.Instance.levelUpText.text = GameConstants.NO_LEVEL;
            UIReferences.Instance.levelUp.gameObject.SetActive(false);
        }
    }

    public void OnStateExit()
    {
        UIReferences.Instance.levelUpBase.gameObject.SetActive(false);
        if (!GameData.isLastLevel)
        {
            UIReferences.Instance.levelUp.onClick.RemoveAllListeners();
        }
        UIReferences.Instance.levelUp.gameObject.SetActive(false);
    }
}
