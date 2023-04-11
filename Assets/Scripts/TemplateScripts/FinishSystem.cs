using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinishSystem : MonoSingleton<FinishSystem>
{
    [Header("Finish_Field")]
    [Space(10)]

    public int finishObject = 0;
    [SerializeField] GameObject _newTimePanel;
    [SerializeField] Button _newTimeButton;

    public void FinishCheck()
    {
        finishObject += 3;
        if (GameManager.Instance.gameStat == GameManager.GameStat.start && ObjectManager.Instance.objectCount <= finishObject)
            StartCoroutine(FinishTime());
    }
    private void NewObjectTime()
    {
        GameManager gameManager = GameManager.Instance;
        Buttons buttons = Buttons.Instance;

        gameManager.addedTime += 2;
        gameManager.SetAddedTime();
        _newTimePanel.SetActive(true);
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
            _newTimeButton.onClick.AddListener(NewObjectTime);
            _newTimePanel.SetActive(true);
        }
        else buttons.winPanel.SetActive(true);
        buttons.finishGameMoneyText.text = moneySystem.NumberTextRevork(gameManager.addedMoney);
        moneySystem.MoneyTextRevork(gameManager.addedMoney);
    }
}
