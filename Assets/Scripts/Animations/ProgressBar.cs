using DG.Tweening;
using Newtonsoft.Json.Linq;
using RPS.Constants;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Image animatedImage;

    private Tween current;

    private void Start()
    {
        UIManager.ProgressUpdated += Progress;
    }

    public void Progress()
    {
        ClearTween();
        float value = GameUtility.RemapValue(GameData.currentProgress, 0, GameConstants.LEVEL_MAXPROGRESS, 0, 1);
        current = animatedImage.DOFillAmount(value, 2f).SetEase(Ease.OutCubic);
    }

    private void ClearTween()
    {
        if (current != null)
            current.Kill();
        current = null;
    }

    private void OnDestroy()
    {
        UIManager.ProgressUpdated -= Progress;
    }

    private void OnDisable()
    {
        ClearTween();
    }
}
