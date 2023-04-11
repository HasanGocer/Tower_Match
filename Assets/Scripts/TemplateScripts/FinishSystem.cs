using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class FinishSystem : MonoSingleton<FinishSystem>
{
    [Header("Finish_Field")]
    [Space(10)]

    public int finishObject = 0;
    [SerializeField] GameObject _newTimePanel;
    [SerializeField] Button _newTimeButton;
    [SerializeField] GameObject _newPerPanel;
    [SerializeField] Button _newPerButton;
    [SerializeField] GameObject _timeGO, _perGO, _timeFinishPos, _perFinishPos;

    public void FinishCheck()
    {
        finishObject += 3;
        if (GameManager.Instance.gameStat == GameManager.GameStat.start && ObjectManager.Instance.objectCount <= finishObject)
            StartCoroutine(FinishTime());
    }
    private IEnumerator NewObjectTime()
    {
        GameManager gameManager = GameManager.Instance;
        Buttons buttons = Buttons.Instance;

        gameManager.addedTime += 2;
        gameManager.SetAddedTime();

        _timeGO.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 1);
        _timeGO.transform.DOMove(_timeFinishPos.transform.position, 1);

        yield return new WaitForSeconds(1);

        buttons.levelPanel.SetActive(false);
        _newTimePanel.SetActive(false);
        buttons.winPanel.SetActive(true);
    }
    private IEnumerator NewObjectPer()
    {
        GameManager gameManager = GameManager.Instance;
        Buttons buttons = Buttons.Instance;

        gameManager.perTime += 2;
        gameManager.SetPerTime();

        _perGO.transform.DOScale(new Vector3(0.3f, 0.3f, 0.3f), 1);
        _perGO.transform.DOMove(_perFinishPos.transform.position, 1);

        yield return new WaitForSeconds(1);

        buttons.levelPanel.SetActive(false);
        _newPerPanel.SetActive(false);
        buttons.winPanel.SetActive(true);
    }
    private IEnumerator FinishTime()
    {
        GameManager gameManager = GameManager.Instance;
        Buttons buttons = Buttons.Instance;
        MoneySystem moneySystem = MoneySystem.Instance;

        gameManager.gameStat = GameManager.GameStat.finish;

        PlacementSystem.Instance.finishTime();
        PlacementSystem.Instance.FinishPartical();
        SoundSystem.Instance.CallFinishSound();


        yield return new WaitForSeconds(2);

        LevelManager.Instance.LevelCheck();
        if (gameManager.level % 5 == 0)
        {
            _newTimeButton.onClick.AddListener(() => StartCoroutine(NewObjectTime()));
            _newTimePanel.SetActive(true);
        }
        else if (gameManager.level % 8 == 0)
        {
            _newPerButton.onClick.AddListener(() => StartCoroutine(NewObjectPer()));
            _newPerPanel.SetActive(true);
        }
        else
        {
            buttons.winPanel.SetActive(true);
            buttons.levelPanel.SetActive(false);
        }
        buttons.finishGameMoneyText.text = moneySystem.NumberTextRevork(gameManager.addedMoney);
        moneySystem.MoneyTextRevork(gameManager.addedMoney);
    }
}
