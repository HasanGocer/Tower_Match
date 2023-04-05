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
    [SerializeField] GameObject _parent;

    public void WrongItem()
    {
        LayerBack();
        if (firstSpace)
        {
            ObjectTouch objectTouch = firstObject.GetComponent<ObjectTouch>();

            firstObject.transform.SetParent(objectTouch.lastPos.transform);
            firstObject.transform.DOMove(objectTouch.lastPos.transform.position, 0.3f);
            firstObject.GetComponent<ObjectTouch>().isFree = false;
            firstObject.transform.DOShakeScale(1, 0.3f);
            firstObject = null;
        }
        if (secondSpace)
        {
            ObjectTouch objectTouch = secondObject.GetComponent<ObjectTouch>();

            secondObject.transform.SetParent(objectTouch.lastPos.transform);
            secondObject.transform.DOMove(objectTouch.lastPos.transform.position, 0.3f);
            secondObject.GetComponent<ObjectTouch>().isFree = false;
            secondObject.transform.DOShakeScale(1, 0.3f);
            Vibration.Vibrate(30);
            secondObject = null;
        }
        if (thridSpace)
        {
            ObjectTouch objectTouch = thridObject.GetComponent<ObjectTouch>();

            thridObject.transform.SetParent(objectTouch.lastPos.transform);
            thridObject.transform.DOMove(objectTouch.lastPos.transform.position, 0.3f);
            thridObject.GetComponent<ObjectTouch>().isFree = false;
            thridObject = null;
        }
        BoolOff();
    }
    public void ObjectCorrect()
    {
        StartCoroutine(MergeTime());
    }
    public IEnumerator MergeTime()
    {
        LayerBack();
        ObjectOff();
        BoolOff();
        Vibration.Vibrate(30);

        firstObject.transform.DOMove(secondObject.transform.position, 0.3f);
        thridObject.transform.DOMove(secondObject.transform.position, 0.3f);

        yield return new WaitForSeconds(0.3f);

        FinishSystem.Instance.FinishCheck();
    }
    public void LayerBack()
    {
        if (firstSpace)
            firstObject.transform.GetChild(tempObjectCount).gameObject.layer = 0;
        if (secondSpace)
            secondObject.transform.GetChild(tempObjectCount).gameObject.layer = 0;
        if (thridSpace)
            thridObject.transform.GetChild(tempObjectCount).gameObject.layer = 0;
    }
    private void ObjectOff()
    {
        firstObject.SetActive(false);
        secondObject.SetActive(false);
        thridObject.SetActive(false);
    }
    private void BoolOff()
    {
        firstSpace = false;
        secondSpace = false;
        thridSpace = false;
    }
}
