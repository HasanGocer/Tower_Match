using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class CoinSystem : MonoSingleton<CoinSystem>
{
    [SerializeField] int _OPCoinCount;
    [SerializeField] int _coinCount;
    [SerializeField] int _comboCount;
    [SerializeField] float _comboSpeed;
    [SerializeField] GameObject _parent, _StartPos, _finishPos;
    [SerializeField] Image _comboBar;
    [SerializeField] TMP_Text _comboText;
    float _barLerpCount;

    public void CoinStart()
    {
        if (_comboCount == 0)
        {
            _comboCount++;
            _comboBar.fillAmount = 1;
            _barLerpCount = 0;
            _comboText.text = _comboCount.ToString();

            StartCoroutine(CoinMove());
            StartCoroutine(ComboBarStart());
        }
        else
        {
            _comboCount++;
            _comboBar.fillAmount = 1;
            _barLerpCount = 0;
            _comboText.text = _comboCount.ToString();

            StartCoroutine(CoinMove());
        }
    }

    private IEnumerator ComboBarStart()
    {
        while (GameManager.Instance.gameStat == GameManager.GameStat.start)
        {
            _barLerpCount += Time.deltaTime * _comboSpeed* _comboCount;
            _comboBar.fillAmount = Mathf.Lerp(_comboBar.fillAmount, 0, _barLerpCount);
            yield return new WaitForSecondsRealtime(Time.deltaTime);
            if (_comboBar.fillAmount <= 0.005)
            {
                _comboCount = 0;
                break;
            }
        }
    }

    private IEnumerator CoinMove()
    {
        List<GameObject> coins = new List<GameObject>();

        for (int i = 0; i < _coinCount + _comboCount; i++)
            coins.Add(ObjectPool.Instance.GetPooledObject(_OPCoinCount, _StartPos.transform.position, _parent.transform));

        foreach (GameObject item in coins)
        {
            item.transform.DOJump(_finishPos.transform.position, Random.Range(0, 10) / 10, Random.Range(0, 10) / 10, 1f);
            MoneySystem.Instance.MoneyTextRevork(Random.Range(2, 8));
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1);
        foreach (GameObject item in coins)
        {
            ObjectPool.Instance.AddObject(_OPCoinCount, item);
            SoundSystem.Instance.CallCoinSound();
        }
    }
}
