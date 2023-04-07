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

        StartCoroutine(Move(floorCount, roomCount, finishFloorCount, finishRoomCount));
        placementSystem.apartment[floorCount, roomCount] = placementSystem.apartment[finishFloorCount, finishRoomCount];
        placementSystem.apartment[finishFloorCount, finishRoomCount] = null;
        placementSystem.floorBool[finishFloorCount, finishRoomCount] = false;
        placementSystem.floorBool[floorCount, roomCount] = true;

        objectID.floorCount = floorCount;
        objectID.roomCount = roomCount;
    }
    private IEnumerator Move(int floorCount, int roomCount, int finishFloorCount, int finishRoomCount)
    {
        PlacementSystem placementSystem = PlacementSystem.Instance;
        GameObject obj = placementSystem.apartment[finishFloorCount, finishRoomCount];
        obj.transform.DOMove(placementSystem.apartmentPos[floorCount, roomCount].transform.position, 0.2f);
        yield return new WaitForSeconds(0.2f);
        obj.transform.position = placementSystem.apartmentPos[floorCount, roomCount].transform.position;
    }
}
