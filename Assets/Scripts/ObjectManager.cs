using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectManager : MonoSingleton<ObjectManager>
{
    public GameObject firstPos, secondPos, thridPos;
    public bool firstSpace, secondSpace, thridSpace;
    public GameObject firstObject, secondObject, thridObject;
    public int tempObjectCount;
    public int objectCount;

    public void WrongItem()
    {
        if (firstSpace)
            firstObject.transform.DOMove(firstObject.GetComponent<ObjectTouch>().lastPos, 0.3f);
        if (secondSpace)
            secondObject.transform.DOMove(secondObject.GetComponent<ObjectTouch>().lastPos, 0.3f);
        if (thridSpace)
            thridObject.transform.DOMove(thridObject.GetComponent<ObjectTouch>().lastPos, 0.3f);
        BoolOff();
    }

    public IEnumerator MergeTime()
    {
        BoolOff();
        firstObject.transform.DOMove(secondObject.transform.position, 0.3f);
        thridObject.transform.DOMove(secondObject.transform.position, 0.3f);
        yield return new WaitForSeconds(0.3f);
        ParticalManager.Instance.ObjectMergePartical(secondObject);
        FinishSystem.Instance.FinishCheck();
    }
    private void BoolOff()
    {
        firstSpace = false;
        secondSpace = false;
        thridSpace = false;
    }
}
