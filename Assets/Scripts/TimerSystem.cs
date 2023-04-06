using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerSystem : MonoSingleton<TimerSystem>
{
    public int maxTimerCount;
    [SerializeField] int _timerCount;
    [SerializeField] GameObject _barPanel;
    [SerializeField] TMP_Text _barText;

    public void StartTimer()
    {
        maxTimerCount = (int)((float)ObjectManager.Instance.objectCount * 1.4f) + 10;
        _timerCount = maxTimerCount;
        StartCoroutine(Timer());
        _barPanel.SetActive(true);
    }

    private IEnumerator Timer()
    {
        for (int i = 0; i < maxTimerCount; i++)
            if (GameManager.Instance.gameStat == GameManager.GameStat.start)
            {
                _timerCount--;
                _barText.text = _timerCount.ToString();
                yield return new WaitForSecondsRealtime(1);
                if (_timerCount == 0 && GameManager.Instance.gameStat == GameManager.GameStat.start)
                {
                    GameManager.Instance.gameStat = GameManager.GameStat.finish;
                    Buttons.Instance.failPanel.SetActive(true);
                    break;
                }
            }
            else break;
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
