using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System;
using UnityEngine;
using UnityEngine.Events;

public class UIMenu : IUIState
{
    public static event Action OnPlayButton;

    public void OnStateEnter()
    {
        UIReferences.Instance.playBase.gameObject.SetActive(true);
        UIReferences.Instance.play.onClick.AddListener(()=> OnPlayButton?.Invoke());
    }

    public void OnStateExit()
    {
        UIReferences.Instance.playBase.gameObject.SetActive(false);
        UIReferences.Instance.play.onClick.RemoveAllListeners();
    }


}

