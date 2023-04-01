using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoSingleton<PlacementSystem>
{
    [SerializeField] int _OPPlacementObject;
    [SerializeField] int _OPSortObject;
    [SerializeField] int _floorCount;
    [SerializeField] GameObject _StartPlacementPos;
    [SerializeField] float _sizeDistance;
    [SerializeField] List<GameObject> _floor = new List<GameObject>();
    [SerializeField] List<GameObject> _Sorts = new List<GameObject>();
    [SerializeField] List<int> _SortsCounts = new List<int>();
    bool[,] floorBool;


    public void StartPlacement()
    {

    }

    public void ObjectPlacement()
    {
        ItemData itemData = ItemData.Instance;

        GameObject floor;
        int childCount, tempFloor, tempRoom;

        for (int i1 = 0; i1 < itemData.field.floorCount; i1++)
        {
            _floor.Add(floor = ObjectPool.Instance.GetPooledObject(GameManager.Instance.level % _floorCount + _OPPlacementObject));
            _objectCount += floor.transform.childCount;
            floorBool = new bool[itemData.field.floorCount, floor.transform.childCount];
        }
        for (int i1 = 0; i1 < _floor.Count; i1++)
        {
            for (int i2 = 0; i2 < _floor[i1].transform.childCount; i2++)
            {
                if (floorBool[i1, i2])
                {
                    childCount = Random.Range(0, ObjectManager.Instance.objectCount);
                    AddObject(i1, i2, childCount);

                    while (!floorBool[tempFloor = Random.Range(0, _floor.Count), tempRoom = Random.Range(0, _floor[0].transform.childCount)]) ;
                    AddObject(tempFloor, tempRoom, childCount);

                    while (!floorBool[tempFloor = Random.Range(0, _floor.Count), tempRoom = Random.Range(0, _floor[0].transform.childCount)]) ;
                    AddObject(tempFloor, tempRoom, childCount);
                }
            }
        }
    }
    private void AddObject(int floorCount, int roomCount, int childCount)
    {
        GameObject pos, obj;

        pos = _floor[floorCount].transform.GetChild(roomCount).gameObject;
        _Sorts.Add(obj = ObjectPool.Instance.GetPooledObject(_OPSortObject, pos.transform.position, pos.transform));
        ObjectID objectID = obj.GetComponent<ObjectID>();
        _SortsCounts.Add(childCount);
        objectID.childs[childCount].SetActive(true);
        objectID.childCount = childCount;
    }
}
