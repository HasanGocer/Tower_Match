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
        if (firstSpace)
        {
            firstObject.transform.DOMove(firstObject.GetComponent<ObjectTouch>().lastPos, 0.3f);
            firstObject.transform.SetParent(_parent.transform);
        }
        if (secondSpace)
        {
            secondObject.transform.DOMove(secondObject.GetComponent<ObjectTouch>().lastPos, 0.3f);
            secondObject.transform.SetParent(_parent.transform);
        }
        if (thridSpace)
        {
            thridObject.transform.DOMove(thridObject.GetComponent<ObjectTouch>().lastPos, 0.3f);
            thridObject.transform.SetParent(_parent.transform);
        }
        LayerBack();
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
        firstObject.transform.DOMove(secondObject.transform.position, 0.3f);
        thridObject.transform.DOMove(secondObject.transform.position, 0.3f);
        yield return new WaitForSeconds(0.3f);
        ParticalManager.Instance.ObjectMergePartical(secondObject);
        FinishSystem.Instance.FinishCheck();
    }
    public void LayerBack()
    {
        if (firstSpace)
            firstObject.gameObject.layer = 0;
        if (secondSpace)
            secondObject.gameObject.layer = 0;
        if (thridSpace)
            thridObject.gameObject.layer = 0;
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
