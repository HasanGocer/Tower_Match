using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectTouch : MonoBehaviour
{
    public Vector3 lastPos;
    [SerializeField] ObjectID objectID;

    private void OnMouseDown()
    {
        if (!ObjectManager.Instance.firstSpace)
        {
            FirstMove();
        }
        else if (!ObjectManager.Instance.secondSpace)
        {
            if (ObjectManager.Instance.tempObjectCount == objectID.childCount)
                SecondMove();
            else
                ObjectManager.Instance.WrongItem();
        }
        else if (!ObjectManager.Instance.thridSpace)
        {
            print(31);
            if (ObjectManager.Instance.tempObjectCount == objectID.childCount)
                StartCoroutine(ThridMove());
            else
                ObjectManager.Instance.WrongItem();
        }
    }
    private void FirstMove()
    {
        ObjectManager objectManager = ObjectManager.Instance;

        lastPos = transform.position;
        objectManager.firstSpace = true;
        objectManager.firstObject = gameObject;
        objectManager.tempObjectCount = objectID.childCount;
        gameObject.transform.GetChild(objectID.childCount).gameObject.layer = 6;
        transform.DOMove(objectManager.firstPos.transform.position, 0.3f);
    }
    private void SecondMove()
    {
        ObjectManager objectManager = ObjectManager.Instance;

        lastPos = transform.position;
        objectManager.secondSpace = true;
        objectManager.secondObject = gameObject;
        gameObject.transform.GetChild(objectID.childCount).gameObject.layer = 6;
        transform.DOMove(objectManager.secondPos.transform.position, 0.3f);
    }
    private IEnumerator ThridMove()
    {
        ObjectManager objectManager = ObjectManager.Instance;
        print(1);
        lastPos = transform.position;
        print(2);
        objectManager.thridSpace = true;
        print(3);
        objectManager.thridObject = gameObject;
        print(4);
        gameObject.transform.GetChild(objectID.childCount).gameObject.layer = 6;
        print(5);
        transform.DOMove(objectManager.thridPos.transform.position, 0.3f);
        print(6);
        yield return new WaitForSeconds(0.3f);
        print(7);
    }

}
