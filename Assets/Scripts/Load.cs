using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load : MonoBehaviour
{
    public static Load Instance;
    public bool isNext;



    void Awake()
    {
        if (Load.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

}
