using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundAnimation : MonoBehaviour
{
    [SerializeField] private Image animatedImage;

    private Tween current;

    public void DoPunch()
    {
        ClearTween();
        current = animatedImage.DOFade(0.7f, 2f).SetEase(Ease.OutBounce);
    }

    public void FadeOut()
    {
        ClearTween();
        current = animatedImage.DOFade(0.0f, 3f).SetEase(Ease.OutQuint);
    }

    private void ClearTween()
    {
        if (current != null)  
            current.Kill();
        current = null;
    }

    private void OnDisable()
    {
        ClearTween();
    }
}
