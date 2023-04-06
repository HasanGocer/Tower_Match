using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [Header("Main_Field")]
    [Space(10)]

    [SerializeField] int floorModCount;

    public void LevelCheck()
    {
        GameManager gameManager = GameManager.Instance;
        ItemData itemData = ItemData.Instance;

        if (gameManager.level % floorModCount == 0)
            itemData.SetFloorCount();
        if (gameManager.level % 5 == 0)
        {
            gameManager.SetAddedTime();
        }
    }
}
