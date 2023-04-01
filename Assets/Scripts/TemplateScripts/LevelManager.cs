using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoSingleton<LevelManager>
{
    [Header("Main_Field")]
    [Space(10)]

    int freeCount;

    public void LevelCheck()
    {
        GameManager gameManager = GameManager.Instance;
        ItemData itemData = ItemData.Instance;

    }
}
