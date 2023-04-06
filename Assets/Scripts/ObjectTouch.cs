using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectTouch : MonoBehaviour
{
    public Transform lastPos;
    [SerializeField] ObjectID objectID;
    public bool isFree;

    private void OnMouseDown()
    {
        ObjectManager objectManager = ObjectManager.Instance;

        if (!objectManager.firstSpace && isFree == false && !objectManager.isFree)
        {
            StartCoroutine(FirstMove());
        }
        else if (!objectManager.secondSpace && isFree == false && !objectManager.isFree)
        {
            if (objectManager.tempObjectCount == objectID.childCount)
                StartCoroutine(SecondMove());
            else
                objectManager.WrongItem();
        }
        else if (!objectManager.thridSpace && isFree == false && !objectManager.isFree)
        {
            if (objectManager.tempObjectCount == objectID.childCount)
                StartCoroutine(ThridMove());
            else
                objectManager.WrongItem();
        }
    }
    private IEnumerator FirstMove()
    {
        ObjectManager objectManager = ObjectManager.Instance;

        objectManager.isFree = true;
        isFree = true;
        lastPos = transform.parent;
        objectManager.firstSpace = true;
        objectManager.firstObject = gameObject;
        objectManager.tempObjectCount = objectID.childCount;
        gameObject.transform.SetParent(objectManager.firstPos.transform);
        gameObject.transform.GetChild(objectID.childCount).gameObject.layer = 6;
        transform.DOScale(transform.lossyScale / 2, 0.3f);
        transform.DOMove(objectManager.firstPos.transform.position, 0.3f);
        yield return new WaitForSeconds(0.3f);
        objectManager.isFree = false;
    }
    private IEnumerator SecondMove()
    {
        ObjectManager objectManager = ObjectManager.Instance;

        objectManager.isFree = true;
        isFree = true;
        lastPos = transform.parent;
        objectManager.secondSpace = true;
        objectManager.secondObject = gameObject;
        gameObject.transform.SetParent(objectManager.secondPos.transform);
        gameObject.transform.GetChild(objectID.childCount).gameObject.layer = 6;
        transform.DOScale(transform.lossyScale / 2, 0.3f);
        transform.DOMove(objectManager.secondPos.transform.position, 0.3f);
        yield return new WaitForSeconds(0.3f);
        objectManager.isFree = false;
    }
    private IEnumerator ThridMove()
    {
        ObjectManager objectManager = ObjectManager.Instance;

        objectManager.isFree = true;
        isFree = true;
        lastPos = transform.parent;
        objectManager.thridSpace = true;
        objectManager.thridObject = gameObject;
        gameObject.transform.SetParent(objectManager.thridPos.transform);
        gameObject.transform.GetChild(objectID.childCount).gameObject.layer = 6;
        transform.localScale = transform.lossyScale / 2;
        transform.DOMove(objectManager.thridPos.transform.position, 0.3f);
        yield return new WaitForSeconds(0.6f);
        objectManager.isFree = false;
        objectManager.ObjectCorrect();
    }

}
