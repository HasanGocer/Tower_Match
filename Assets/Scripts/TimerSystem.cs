using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerSystem : MonoSingleton<TimerSystem>
{
    [SerializeField] int _timerCount, _maxTimerCount, _barSpeed;
    [SerializeField] Image _bar;
    [SerializeField] GameObject _barPanel;

    public void StartTimer()
    {
        _timerCount = _maxTimerCount;
        StartCoroutine(Timer());
        StartCoroutine(BarTimer());
        _barPanel.SetActive(true);
    }

    private IEnumerator Timer()
    {
        for (int i = 0; i < _maxTimerCount; i++)
        {
            _timerCount--;
            yield return new WaitForSecondsRealtime(1);
        }
    }

    private IEnumerator BarTimer()
    {
        while (true)
        {
            _bar.fillAmount = Mathf.Lerp(_bar.fillAmount, (float)_timerCount / (float)_maxTimerCount, Time.deltaTime * _barSpeed);
            yield return new WaitForEndOfFrame();
            if (_timerCount == _maxTimerCount)
            {
                GameManager.Instance.gameStat = GameManager.GameStat.finish;
                Buttons.Instance.failPanel.SetActive(true);
                break;
            }
        }
    }
}
