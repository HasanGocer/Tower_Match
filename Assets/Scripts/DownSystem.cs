using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DownSystem : MonoSingleton<DownSystem>
{
    public void AllDown(int firstFloorCount, int firstRoomCount, int secondFloorCount, int secondRoomCount, int thirsthFloorCount, int thirsthRoomCount)
    {
        DownTime(firstFloorCount, firstRoomCount);
        DownTime(secondFloorCount, secondRoomCount);
        DownTime(thirsthFloorCount, thirsthRoomCount);
    }
    void DownTime(int floorCount, int roomCount)
    {
        ObjectManager.Instance.isFree = true;
        PlacementSystem placementSystem = PlacementSystem.Instance;

        bool isFinish = false;
        for (int i = floorCount + 1; i < placementSystem.floor.Count; i++)
            if (placementSystem.floorBool[i, roomCount])
            {
                ChangeRoom(floorCount, roomCount, i, roomCount, ref isFinish);
                if (floorCount != placementSystem.floor.Count - 1)
                    DownTime(floorCount + 1, roomCount);
                break;
            }

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
            ObjectManager.Instance.isFree = false;
    }

    private void ChangeRoom(int floorCount, int roomCount, int finishFloorCount, int finishRoomCount, ref bool isFinish)
    {
        PlacementSystem placementSystem = PlacementSystem.Instance;
        ObjectID objectID = placementSystem.apartment[floorCount, roomCount].GetComponent<ObjectID>();

        isFinish = true;
        placementSystem.apartment[finishFloorCount, finishRoomCount].transform.DOMove(placementSystem.apartmentPos[floorCount, roomCount].transform.position, 0.2f);
        placementSystem.apartment[floorCount, roomCount] = placementSystem.apartment[finishFloorCount, finishRoomCount];
        placementSystem.apartment[finishFloorCount, finishRoomCount] = null;
        placementSystem.floorBool[finishFloorCount, finishRoomCount] = false;
        placementSystem.floorBool[floorCount, roomCount] = true;

        objectID.floorCount = floorCount;
        objectID.roomCount = roomCount;

        ObjectManager.Instance.isFree = false;
    }

}
