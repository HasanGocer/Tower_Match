using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DownSystem : MonoSingleton<DownSystem>
{
    public void AllDown(ObjectID firstObjectID, ObjectID secondObjectID, ObjectID thirsthObjectID)
    {
        DownTime(firstObjectID.floorCount, firstObjectID.roomCount);
        DownTime(secondObjectID.floorCount, secondObjectID.roomCount);
        DownTime(thirsthObjectID.floorCount, thirsthObjectID.roomCount);
    }
    void DownTime(int floorCount, int roomCount)
    {
        PlacementSystem placementSystem = PlacementSystem.Instance;
        bool isFinish = false;

        if (!placementSystem.floorBool[floorCount, roomCount])
            for (int i = floorCount + 1; i < placementSystem.floor.Count; i++)
                if (placementSystem.floorBool[i, roomCount])
                {
                    ChangeRoom(floorCount, roomCount, i, roomCount, ref isFinish);
                    if (floorCount + 1 != placementSystem.floor.Count - 1)
                        DownTime(floorCount + 1, roomCount);
                    break;
                }

        /*
        if (!isFinish)
            for (int i = placementSystem.floor.Count - 1; i > floorCount + 1; i--)
            {
                if (roomCount == 0)
                {
                    if (placementSystem.floorBool[i, placementSystem.floor[0].transform.childCount - 1])
                    {
                        ChangeRoom(floorCount, roomCount, i, placementSystem.floor[0].transform.childCount - 1, ref isFinish);
                        break;
                    }
                    else if (placementSystem.floorBool[i, roomCount + 1])
                    {
                        ChangeRoom(floorCount, roomCount, i, roomCount + 1, ref isFinish);
                        break;
                    }
                }
                else if (roomCount == placementSystem.floor[0].transform.childCount - 1)
                {
                    if (placementSystem.floorBool[i, roomCount - 1])
                    {
                        ChangeRoom(floorCount, roomCount, i, roomCount - 1, ref isFinish);
                        break;
                    }
                    else if (placementSystem.floorBool[i, 0])
                    {
                        ChangeRoom(floorCount, roomCount, i, 0, ref isFinish);
                        break;
                    }
                }
                else
                {
                    if (placementSystem.floorBool[i, roomCount - 1])
                    {
                        ChangeRoom(floorCount, roomCount, i, roomCount - 1, ref isFinish);
                        break;
                    }
                    else if (placementSystem.floorBool[i, roomCount + 1])
                    {
                        ChangeRoom(floorCount, roomCount, i, roomCount + 1, ref isFinish);
                        break;
                    }
                }
            }
        
        if (!isFinish)
            ObjectManager.Instance.isFree = false;*/
    }

    private void ChangeRoom(int floorCount, int roomCount, int finishFloorCount, int finishRoomCount, ref bool isFinish)
    {
        PlacementSystem placementSystem = PlacementSystem.Instance;
        ObjectID objectID = placementSystem.apartment[finishFloorCount, finishRoomCount].GetComponent<ObjectID>();

        isFinish = true;

        placementSystem.apartment[floorCount, roomCount] = placementSystem.apartment[finishFloorCount, finishRoomCount];
        placementSystem.apartment[finishFloorCount, finishRoomCount] = null;
        placementSystem.floorBool[finishFloorCount, finishRoomCount] = false;
        placementSystem.floorBool[floorCount, roomCount] = true;

        objectID.floorCount = floorCount;
        objectID.roomCount = roomCount;

        Move(floorCount, roomCount, objectID);
    }
    private void Move(int finishFloorCount, int finishRoomCount, ObjectID objectID)
    {
        PlacementSystem placementSystem = PlacementSystem.Instance;
        GameObject obj = placementSystem.apartment[finishFloorCount, finishRoomCount];
        ObjectTouch objectTouch = obj.GetComponent<ObjectTouch>();

        StartCoroutine(Move(obj, objectTouch, objectID));
    }
    private IEnumerator Move(GameObject moveObj, ObjectTouch objectTouch, ObjectID objectID)
    {
        PlacementSystem placementSystem = PlacementSystem.Instance;

        float lerpCount = 0;
        objectTouch.isDown = true;

        while (objectTouch.isDown)
        {
            lerpCount += Time.deltaTime * 5;
            moveObj.transform.position = Vector3.Lerp(moveObj.transform.position, placementSystem.apartmentPos[objectID.floorCount, objectID.roomCount].transform.position, lerpCount);
            yield return new WaitForSeconds(Time.deltaTime);
            if (0.001f > Vector3.Distance(transform.position, placementSystem.apartmentPos[objectID.floorCount, objectID.roomCount].transform.position))
                break;
        }
        objectTouch.isDown = false;
    }
}
