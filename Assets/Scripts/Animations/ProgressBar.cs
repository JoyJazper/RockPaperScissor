using DG.Tweening;
using Newtonsoft.Json.Linq;
using RPS.Constants;
using RPS.Models;
using RPS.Systems;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image animatedImage;

    private Tween current;

    private void Start()
    {
        GameData.currentProgress.OnValueChanged += Progress;
        GameData.OnGameDataDestroy += DisableListeners;
        Progress(GameData.currentProgress.Value);
    }

    private void Progress(float progress)
    {
        float value = GameUtility.RemapValue(progress, 0, GameConstants.LEVEL_MAXPROGRESS, 0, 1);
        current = animatedImage.DOFillAmount(value, 2f).SetEase(Ease.OutCubic).OnComplete(ClearTween);
    }

    private void ClearTween()
    {
        if (current != null)
        {
            current.Kill();
            current = null;
        }
    }
    
    private void DisableListeners()
    {
        GameData.currentProgress.OnValueChanged -= Progress;
        GameData.OnGameDataDestroy -= DisableListeners;
    }

    private void OnDisable()
    {
        ClearTween();
    }
}
