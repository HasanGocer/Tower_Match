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
    [SerializeField] GameObject _timePanel;
    [SerializeField] Button _addTimer;
    [SerializeField] int _addedTime;

    public void StartTimer()
    {
        maxTimerCount = (int)((float)ObjectManager.Instance.objectCount * 1.15f)+ 5;
        _timerCount = maxTimerCount;
        StartCoroutine(Timer());
        _barPanel.SetActive(true);
        _timePanel.SetActive(true);
        _addTimer.onClick.AddListener(AddedTime);
    }

    private void AddedTime()
    {
        if (GameManager.Instance.addedTime > 0)
        {
            GameManager.Instance.addedTime--;
            _timerCount += _addedTime;
            _barText.text = _timerCount.ToString();
        }
    }

    private IEnumerator Timer()
    {
        while (true)
            if (GameManager.Instance.gameStat == GameManager.GameStat.start)
            {
                _timerCount--;
                _barText.text = _timerCount.ToString();
                yield return new WaitForSeconds(1);
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
