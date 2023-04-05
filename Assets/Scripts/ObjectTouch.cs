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
        if (!ObjectManager.Instance.firstSpace && isFree == false)
        {
            FirstMove();
        }
        else if (!ObjectManager.Instance.secondSpace && isFree == false)
        {
            if (ObjectManager.Instance.tempObjectCount == objectID.childCount)
                SecondMove();
            else
                ObjectManager.Instance.WrongItem();
        }
        else if (!ObjectManager.Instance.thridSpace && isFree == false)
        {
            if (ObjectManager.Instance.tempObjectCount == objectID.childCount)
                StartCoroutine(ThridMove());
            else
                ObjectManager.Instance.WrongItem();
        }
    }
    private void FirstMove()
    {
        ObjectManager objectManager = ObjectManager.Instance;

        isFree = true;
        lastPos = transform.parent;
        objectManager.firstSpace = true;
        objectManager.firstObject = gameObject;
        objectManager.tempObjectCount = objectID.childCount;
        gameObject.transform.SetParent(objectManager.firstPos.transform);
        gameObject.transform.GetChild(objectID.childCount).gameObject.layer = 6;
        transform.DOMove(objectManager.firstPos.transform.position, 0.3f);
    }
    private void SecondMove()
    {
        ObjectManager objectManager = ObjectManager.Instance;

        isFree = true;
        lastPos = transform.parent;
        objectManager.secondSpace = true;
        objectManager.secondObject = gameObject;
        gameObject.transform.SetParent(objectManager.secondPos.transform);
        gameObject.transform.GetChild(objectID.childCount).gameObject.layer = 6;
        transform.DOMove(objectManager.secondPos.transform.position, 0.3f);
    }
    private IEnumerator ThridMove()
    {
        ObjectManager objectManager = ObjectManager.Instance;

        isFree = true;
        lastPos = transform.parent;
        objectManager.thridSpace = true;
        objectManager.thridObject = gameObject;
        gameObject.transform.SetParent(objectManager.thridPos.transform);
        gameObject.transform.GetChild(objectID.childCount).gameObject.layer = 6;
        transform.DOMove(objectManager.thridPos.transform.position, 0.3f);
        yield return new WaitForSeconds(0.3f);
        objectManager.ObjectCorrect();
    }

}
