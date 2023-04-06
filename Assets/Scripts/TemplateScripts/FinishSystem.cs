using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishSystem : MonoSingleton<FinishSystem>
{
    [Header("Finish_Field")]
    [Space(10)]

    public int finishObject = 0;

    public void FinishCheck()
    {
        finishObject += 3;
        if (GameManager.Instance.gameStat == GameManager.GameStat.start && ObjectManager.Instance.objectCount <= finishObject)
            FinishTime();
    }
    private void FinishTime()
    {
        GameManager gameManager = GameManager.Instance;
        Buttons buttons = Buttons.Instance;
        MoneySystem moneySystem = MoneySystem.Instance;
        StartCoroutine(BarSystem.Instance.BarImageFillAmountIenum());
        LevelManager.Instance.LevelCheck();
        PlacementSystem.Instance.FinishPartical();
        PlacementSystem.Instance.finishTime();
        buttons.winPanel.SetActive(true);
        SoundSystem.Instance.CallFinishSound();
        buttons.barPanel.SetActive(true);
        buttons.finishGameMoneyText.text = moneySystem.NumberTextRevork(gameManager.addedMoney);
        gameManager.gameStat = GameManager.GameStat.finish;
        moneySystem.MoneyTextRevork(gameManager.addedMoney);
    }
}
