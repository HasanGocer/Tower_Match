using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoSingleton<ObjectManager>
{
    public GameObject firstPos, secondPos, thirdPos;
    public bool firstSpace, secondSpace, thridSpace;
    public GameObject firstObject, secondObject, thirdObject;
    public int tempObjectCount;
    public int objectCount;

    public void WrongItem()
    {

    }
}
