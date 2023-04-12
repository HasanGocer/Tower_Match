using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ObjectTouch : MonoBehaviour
{
    public Transform lastPos;
    [SerializeField] ObjectID objectID;
    public bool isFree, isSelected, isDown;

    private void OnMouseDown()
    {
        Touch();
    }
    public void Touch()
    {
        ObjectManager objectManager = ObjectManager.Instance;

        if (GameManager.Instance.gameStat == GameManager.GameStat.start)
        {
            if (!objectManager.firstSpace && isFree == false && !objectManager.isFree)
            {
                FirstMove();
            }
            else if (!objectManager.secondSpace && isFree == false && !objectManager.isFree)
            {
                if (objectManager.tempObjectCount == objectID.childCount)
                    SecondMove();
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

    private void FirstMove()
    {
        ObjectManager objectManager = ObjectManager.Instance;

        objectManager.isFree = true;
        isFree = true;
        isDown = false;
        isSelected = true;
        lastPos = PlacementSystem.Instance.apartmentPos[objectID.floorCount, objectID.roomCount].transform;
        objectManager.firstSpace = true;
        objectManager.firstObject = gameObject;
        objectManager.tempObjectCount = objectID.childCount;
        gameObject.transform.SetParent(objectManager.firstPos.transform);
        gameObject.transform.GetChild(objectID.childCount).gameObject.layer = 6;
        StartCoroutine(Move(objectManager.firstPos));
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        SoundSystem.Instance.CallObjectTouchSound();
        objectManager.isFree = false;
    }
    private void SecondMove()
    {
        ObjectManager objectManager = ObjectManager.Instance;

        objectManager.isFree = true;
        isFree = true;
        isDown = false;
        isSelected = true;
        lastPos = PlacementSystem.Instance.apartmentPos[objectID.floorCount, objectID.roomCount].transform;
        objectManager.secondSpace = true;
        objectManager.secondObject = gameObject;
        gameObject.transform.SetParent(objectManager.secondPos.transform);
        gameObject.transform.GetChild(objectID.childCount).gameObject.layer = 6;
        StartCoroutine(Move(objectManager.secondPos));
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        SoundSystem.Instance.CallObjectTouchSound();
        objectManager.isFree = false;
    }
    private IEnumerator ThridMove()
    {
        ObjectManager objectManager = ObjectManager.Instance;

        objectManager.isFree = true;
        isFree = true;
        isDown = false;
        isSelected = true;
        lastPos = PlacementSystem.Instance.apartmentPos[objectID.floorCount, objectID.roomCount].transform;
        objectManager.thridSpace = true;
        objectManager.thridObject = gameObject;
        gameObject.transform.SetParent(objectManager.thridPos.transform);
        gameObject.transform.GetChild(objectID.childCount).gameObject.layer = 6;
        StartCoroutine(Move(objectManager.thridPos));
        transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        SoundSystem.Instance.CallObjectTouchSound();
        yield return new WaitForSeconds(0.3f);
        objectManager.isFree = false;
        objectManager.ObjectCorrect();
    }
    private IEnumerator Move(GameObject finishPos)
    {
        float lerpCount = 0;

        while (isSelected)
        {
            lerpCount += Time.deltaTime * 5;
            transform.position = Vector3.Lerp(transform.position, finishPos.transform.position, lerpCount);
            yield return new WaitForSeconds(Time.deltaTime);
            if (0.01f > Vector3.Distance(transform.position, finishPos.transform.position))
                break;
        }
    }

}
