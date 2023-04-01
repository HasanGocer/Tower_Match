using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalManager : MonoSingleton<ParticalManager>
{
    [SerializeField] int _OPObjectMergeParticalCount;

    public void ObjectMergePartical(GameObject pos)
    {
        ObjectPool.Instance.GetPooledObjectAdd(_OPObjectMergeParticalCount, pos.transform.position);
    }
}
