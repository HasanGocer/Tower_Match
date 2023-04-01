using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementSystem : MonoSingleton<PlacementSystem>
{
    [SerializeField] GameObject _StartPlacementPos;
    [SerializeField] float _sizeDistance;

    public void StartPlacement()
    {

    }

    public void ObjectPlacement()
    {
        ItemData itemData = ItemData.Instance;

        int size = 1 + (itemData.field.sizeCount / 2);
        bool corner = (itemData.field.sizeCount / 2) % 2 == 0 ? false : true;

        for (int i1 = 0; i1 < itemData.field.floorCount; i1++)
            for (int i2 = 0; i2 < itemData.field.sizeCount; i2++)
                for (int i3 = 0; i3 < 1 + (i2 * 2); i3++)
                {

                }
    }

}
