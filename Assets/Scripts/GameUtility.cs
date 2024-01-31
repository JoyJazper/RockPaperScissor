using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameUtility : Singleton<GameUtility>
{
    public void DelayOneFrame(UnityAction OnUpdate = null)
    {
        StartCoroutine(InvokeAfterOneFrame(OnUpdate));
    }

    private IEnumerator InvokeAfterOneFrame(UnityAction OnUpdate = null)
    {
        yield return null;
        OnUpdate?.Invoke();
    }

    public void DelayFor(float time, UnityAction timerStopEvent)
    {
        StartCoroutine(InvokeAfter(time, timerStopEvent));
    }

    private IEnumerator InvokeAfter(float time, UnityAction timerStopEvent)
    {
        yield return new WaitForSeconds(time);
        timerStopEvent?.Invoke();
    }

    public static float RemapValue(float value, float minValue, float maxValue, float min, float max)
    {
        return Mathf.Clamp01((value - minValue) / (maxValue - minValue)) * (max - min) + min;
    }

    protected override void OnDestroy()
    {
        StopAllCoroutines();
        base.OnDestroy();
    }
}


