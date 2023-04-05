using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerSystem : MonoSingleton<TimerSystem>
{
    [SerializeField] int _timerCount, _maxTimerCount;
    [SerializeField] GameObject _barPanel;
    [SerializeField] TMP_Text _barText;

    public void StartTimer()
    {
        _timerCount = _maxTimerCount;
        StartCoroutine(Timer());
        _barPanel.SetActive(true);
    }

    private IEnumerator Timer()
    {
        for (int i = 0; i < _maxTimerCount; i++)
        {
            _timerCount--;
            _barText.text = _timerCount.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
    }

    /*  private IEnumerator BarTimer()
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
      }*/
}
