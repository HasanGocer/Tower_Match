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

        if (GameManager.Instance.gameStat == GameManager.GameStat.start)
        {
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
    }
    private IEnumerator FirstMove()
    {
        ObjectManager objectManager = ObjectManager.Instance;

        objectManager.isFree = true;
        isFree = true;
        lastPos = PlacementSystem.Instance.apartmentPos[objectID.floorCount, objectID.roomCount].transform;
        objectManager.firstSpace = true;
        objectManager.firstObject = gameObject;
        objectManager.tempObjectCount = objectID.childCount;
        gameObject.transform.SetParent(objectManager.firstPos.transform);
        gameObject.transform.GetChild(objectID.childCount).gameObject.layer = 6;
        transform.DOMove(objectManager.firstPos.transform.position, 0.3f);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        SoundSystem.Instance.CallObjectTouchSound();
        yield return new WaitForSeconds(0.3f);
        transform.localScale = new Vector3(0.035f, 0.035f, 0.035f);
        objectManager.isFree = false;
    }
    private IEnumerator SecondMove()
    {
        ObjectManager objectManager = ObjectManager.Instance;

        objectManager.isFree = true;
        isFree = true;
        lastPos = PlacementSystem.Instance.apartmentPos[objectID.floorCount, objectID.roomCount].transform;
        objectManager.secondSpace = true;
        objectManager.secondObject = gameObject;
        gameObject.transform.SetParent(objectManager.secondPos.transform);
        gameObject.transform.GetChild(objectID.childCount).gameObject.layer = 6;
        transform.DOMove(objectManager.secondPos.transform.position, 0.3f);
        transform.localScale = new Vector3(0.035f, 0.035f, 0.035f);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        SoundSystem.Instance.CallObjectTouchSound();
        yield return new WaitForSeconds(0.3f);
        transform.localScale = new Vector3(0.035f, 0.035f, 0.035f);
        objectManager.isFree = false;
    }
    private IEnumerator ThridMove()
    {
        ObjectManager objectManager = ObjectManager.Instance;

        objectManager.isFree = true;
        isFree = true;
        lastPos = PlacementSystem.Instance.apartmentPos[objectID.floorCount, objectID.roomCount].transform;
        objectManager.thridSpace = true;
        objectManager.thridObject = gameObject;
        gameObject.transform.SetParent(objectManager.thridPos.transform);
        gameObject.transform.GetChild(objectID.childCount).gameObject.layer = 6;
        transform.DOMove(objectManager.thridPos.transform.position, 0.3f);
        transform.rotation = Quaternion.Euler(Vector3.zero);
        SoundSystem.Instance.CallObjectTouchSound();
        yield return new WaitForSeconds(0.6f);
        transform.localScale = new Vector3(0.035f, 0.035f, 0.035f);
        objectManager.isFree = false;
        objectManager.ObjectCorrect();
    }

}
