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
        isFree = true;
        SoundSystem.Instance.CallWrongObjectSound();
        LayerBack();
        if (firstSpace)
            StartCoroutine(WrongFirstObject());
        if (secondSpace)
            StartCoroutine(WrongSecondObject());
        if (thridSpace)
            WrongThristhObject();
        BoolOff();
        isFree = false;
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

        List<GameObject> objs = new List<GameObject>();
        objs.Add(firstObject);
        objs.Add(secondObject);
        objs.Add(thridObject);

        firstObject.transform.DOMove(secondObject.transform.position, 0.3f);
        thridObject.transform.DOMove(secondObject.transform.position, 0.3f);
        ParticalManager.Instance.CallObjectMergePartical(secondObject);

        ObjectOff();
        isFree = false;

        yield return new WaitForSeconds(0.3f);

        foreach (GameObject item in objs) item.SetActive(false);
        CoinSystem.Instance.CoinStart();
        FinishSystem.Instance.FinishCheck();
    }
    private void LayerBack()
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
        GameObject tempObject = firstObject;
        firstObject = null;

        objectTouch.isSelected = false;
        tempObject.transform.DOShakeScale(0.2f, 0.05f);
        yield return new WaitForSeconds(0.25f);
        tempObject.transform.SetParent(objectTouch.lastPos.transform);
        StartCoroutine(Move(tempObject, objectTouch.lastPos.gameObject));
        tempObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        objectTouch.isFree = false;
    }
    private IEnumerator WrongSecondObject()
    {
        ObjectTouch objectTouch = secondObject.GetComponent<ObjectTouch>();
        GameObject tempObject = secondObject;
        secondObject = null;

        objectTouch.isSelected = false;
        tempObject.transform.DOShakeScale(0.2f, 0.05f);
        yield return new WaitForSeconds(0.25f);
        tempObject.transform.SetParent(objectTouch.lastPos.transform);
        StartCoroutine(Move(tempObject, objectTouch.lastPos.gameObject));
        tempObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        objectTouch.isFree = false;
        Vibration.Vibrate(30);
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
        firstObject = null;
        secondObject = null;
        thridObject = null;
    }
    private void BoolOff()
    {
        firstSpace = false;
        secondSpace = false;
        thridSpace = false;
    }
    private IEnumerator Move(GameObject moveObj, GameObject finishPos)
    {
        ObjectTouch objectTouch = moveObj.GetComponent<ObjectTouch>();
        float lerpCount = 0;

        while (!objectTouch.isSelected)
        {
            lerpCount += Time.deltaTime * 5;
            moveObj.transform.position = Vector3.Lerp(moveObj.transform.position, finishPos.transform.position, lerpCount);
            yield return new WaitForSeconds(Time.deltaTime);
            if (0.01f > Vector3.Distance(transform.position, finishPos.transform.position))
                break;
        }
    }
}
