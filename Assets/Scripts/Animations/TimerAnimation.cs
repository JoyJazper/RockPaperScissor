using System.Collections;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.Events;

public class TimerAnimation : MonoBehaviour
{
    [SerializeField] private TMP_Text Counter;

    [Header("Text Animation Timings")]
    private const float MaxScale = 1.05f; 
    private const float scaleDuration = 0.2f;
    private const float calculationCycleDuration = 0.1f;
    private const float bufferTime = 0.3f;

    private float targetTime;

    private int time = 0;
    private float updatedTime = 0f;
    private float initialTime = 0f;
    
    internal void Init(float target, UnityAction onAnimationComplete)
    {
        ResetAnimator();
        targetTime = target;
        Counter.text = "";
        StartTextAnimation(onAnimationComplete);
    }

    Coroutine current = null;

    private void StartTextAnimation(UnityAction onAnimationComplete)
    {
        initialTime = Time.time;
        current = StartCoroutine(UpdateTimeFor(onAnimationComplete));
    }

    private IEnumerator UpdateTimeFor(UnityAction onAnimationComplete)
    {
        while(updatedTime <=  targetTime)
        {
            updatedTime = Time.time - initialTime;
            if (time != (int)updatedTime)
            {
                UpdateText();
                time = (int)updatedTime;
            }
            yield return new WaitForSeconds(calculationCycleDuration);
        }
        yield return new WaitForSeconds(bufferTime);
        onAnimationComplete();
        initialTime = 0;
        updatedTime = 0;
    }

    private Tween currentTween;

    private void UpdateText()
    {
        Counter.text = ((int)targetTime - (time+1)).ToString();
        Vector3 temp = Counter.transform.localScale;
        temp.x *= MaxScale; 
        temp.y *= MaxScale; 
        temp.z *= MaxScale;
        currentTween = Counter.rectTransform.DOPunchScale(temp, scaleDuration);
        AudioManager.Instance.PlaySFX(AudioClipID.CountdownSFX);
    }

    private void ResetAnimator()
    {
        if (current != null)
        {
            StopCoroutine(current);
            current = null;
        }
        if (currentTween != null)
        {
            currentTween.Kill();
            currentTween = null;
        }
        Counter.text = "";
        targetTime = 0;
        time = 0;
    }

    private void OnDisable()
    {
        ResetAnimator();
    }
}