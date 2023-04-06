using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoSingleton<PlacementSystem>
{
    [SerializeField] int _OPPlacementObject;
    [SerializeField] int _OPSortObject;
    [SerializeField] int _floorCount;
    [SerializeField] int _objectCount;
    [SerializeField] float _floorDistance;
    [SerializeField] GameObject _StartPlacementPos;
    public List<GameObject> floor = new List<GameObject>();
    [SerializeField] List<int> _SortsCounts = new List<int>();
    public bool[,] floorBool;
    public GameObject[,] apartment, apartmentPos;

    public void FinishPartical()
    {
        ParticalManager.Instance.CalLFinishPartical(_StartPlacementPos);
    }
    public void StartPlacement()
    {
        ObjectPlacement();
    }
    public void finishTime()
    {
        _StartPlacementPos.SetActive(false);
    }

    private void ObjectPlacement()
    {
        ItemData itemData = ItemData.Instance;

        GameObject floorTemp;
        int childCount, tempFloor, tempRoom;

        for (int i1 = 0; i1 < itemData.field.floorCount; i1++)
        {
            floor.Add(floorTemp = ObjectPool.Instance.GetPooledObject(GameManager.Instance.level % _floorCount + _OPPlacementObject, new Vector3(_StartPlacementPos.transform.position.x, _StartPlacementPos.transform.position.y + _floorDistance * floor.Count, _StartPlacementPos.transform.position.z), _StartPlacementPos.transform));
            ObjectManager.Instance.objectCount += floorTemp.transform.childCount;
        }

        CameraMove.Instance.maxTargetPos.y = itemData.field.floorCount * _floorDistance;

        floorBool = new bool[itemData.field.floorCount, floor[0].transform.childCount];
        apartment = new GameObject[itemData.field.floorCount, floor[0].transform.childCount];
        apartmentPos = new GameObject[itemData.field.floorCount, floor[0].transform.childCount];

        for (int i1 = 0; i1 < floor.Count; i1++)
        {
            for (int i2 = 0; i2 < floor[i1].transform.childCount; i2++)
            {
                if (!floorBool[i1, i2])
                {
                    childCount = Random.Range(0, _objectCount);
                    AddObject(i1, i2, childCount);

                    while (floorBool[tempFloor = Random.Range(0, floor.Count), tempRoom = Random.Range(0, floor[0].transform.childCount)]) ;
                    AddObject(tempFloor, tempRoom, childCount);

                    while (floorBool[tempFloor = Random.Range(0, floor.Count), tempRoom = Random.Range(0, floor[0].transform.childCount)]) ;
                    AddObject(tempFloor, tempRoom, childCount);
                }
            }
        }
    }
    private void AddObject(int floorCount, int roomCount, int childCount)
    {
        GameObject pos, obj;

        floorBool[floorCount, roomCount] = true;
        pos = floor[floorCount].transform.GetChild(roomCount).gameObject;
        apartment[floorCount, roomCount] = obj = ObjectPool.Instance.GetPooledObject(_OPSortObject, pos.transform.position, pos.transform);
        apartmentPos[floorCount, roomCount] = floor[floorCount].transform.GetChild(roomCount).gameObject;
        ObjectID objectID = obj.GetComponent<ObjectID>();
        obj.transform.SetParent(floor[floorCount].transform.GetChild(roomCount).transform);
        _SortsCounts.Add(childCount);
        objectID.childs[childCount].SetActive(true);

        objectID.floorCount = floorCount;
        objectID.roomCount = roomCount;
        objectID.childCount = childCount;
    }
}
