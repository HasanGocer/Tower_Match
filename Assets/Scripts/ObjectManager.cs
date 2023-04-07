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
    public bool isFree;

    public void WrongItem()
    {
        SoundSystem.Instance.CallWrongObjectSound();
        LayerBack();
        if (firstSpace)
            StartCoroutine(WrongFirstObject());
        if (secondSpace)
            StartCoroutine(WrongSecondObject());
        if (thridSpace)
            WrongThristhObject();
        BoolOff();
    }
    public void ObjectCorrect()
    {
        StartCoroutine(MergeTime());
    }
    public IEnumerator MergeTime()
    {
        isFree = true;
        LayerBack();
        BoolCheck();
        BoolOff();
        Vibration.Vibrate(30);

        firstObject.transform.DOMove(secondObject.transform.position, 0.3f);
        thridObject.transform.DOMove(secondObject.transform.position, 0.3f);
        ParticalManager.Instance.CallObjectMergePartical(secondObject);

        ObjectOff();
        isFree = false;

        yield return new WaitForSeconds(0.3f);

        CoinSystem.Instance.CoinStart();
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
    private IEnumerator WrongFirstObject()
    {
        ObjectTouch objectTouch = firstObject.GetComponent<ObjectTouch>();

        firstObject.transform.DOShakeScale(0.25f, 0.05f);
        firstObject.transform.SetParent(objectTouch.lastPos.transform);
        yield return new WaitForSecondsRealtime(0.3f);
        firstObject.transform.DOMove(objectTouch.lastPos.transform.position, 0.3f);
        objectTouch.isFree = false;
        firstObject = null;
    }
    private IEnumerator WrongSecondObject()
    {
        ObjectTouch objectTouch = secondObject.GetComponent<ObjectTouch>();

        secondObject.transform.DOShakeScale(0.25f, 0.05f);
        secondObject.transform.SetParent(objectTouch.lastPos.transform);
        yield return new WaitForSecondsRealtime(0.3f);
        secondObject.transform.DOMove(objectTouch.lastPos.transform.position, 0.3f);
        objectTouch.isFree = false;
        Vibration.Vibrate(30);
        secondObject = null;
    }
    private void WrongThristhObject()
    {
        ObjectTouch objectTouch = thridObject.GetComponent<ObjectTouch>();

        thridObject.transform.SetParent(objectTouch.lastPos.transform);
        thridObject.transform.DOMove(objectTouch.lastPos.transform.position, 0.3f);
        objectTouch.isFree = false;
        thridObject = null;
    }
    private void BoolCheck()
    {
        ObjectID firstObjectID = firstObject.GetComponent<ObjectID>();
        ObjectID secondObjectID = secondObject.GetComponent<ObjectID>();
        ObjectID thridObjectID = thridObject.GetComponent<ObjectID>();

        PlacementSystem.Instance.floorBool[firstObjectID.floorCount, firstObjectID.roomCount] = false;
        PlacementSystem.Instance.floorBool[secondObjectID.floorCount, secondObjectID.roomCount] = false;
        PlacementSystem.Instance.floorBool[thridObjectID.floorCount, thridObjectID.roomCount] = false;

        DownSystem.Instance.AllDown(firstObjectID, secondObjectID, thridObjectID);
    }
    private void ObjectOff()
    {
        firstObject.SetActive(false);
        firstObject = null;
        secondObject.SetActive(false);
        secondObject = null;
        thridObject.SetActive(false);
        thridObject = null;
    }
    private void BoolOff()
    {
        firstSpace = false;
        secondSpace = false;
        thridSpace = false;
    }
}
