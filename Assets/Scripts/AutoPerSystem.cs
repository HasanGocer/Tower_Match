using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AutoPerSystem : MonoSingleton<AutoPerSystem>
{
    [SerializeField] Button _perButton;
    [SerializeField] TMP_Text _perCountText;

    GameObject firstObject, secondObject, thristhObject;

    public void PerStart()
    {

    }

    private IEnumerator PerTime()
    {
        PlacementSystem placementSystem = PlacementSystem.Instance;
        ItemData itemData = ItemData.Instance;

        int objectCount = 0;

        for (int i1 = 0; i1 < itemData.field.floorCount; i1++)
            for (int i2 = 0; i2 < placementSystem.floor[0].transform.childCount; i2++)
                if (placementSystem.floorBool[i1, i2])
                {
                    firstObject = placementSystem.floor[i1].transform.GetChild(i2).gameObject;
                    objectCount = placementSystem.floor[i1].transform.GetChild(i2).GetComponent<ObjectID>().childCount;
                    i2 = placementSystem.floor[0].transform.childCount;
                    i1 = itemData.field.floorCount;
                }
        for (int i1 = 0; i1 < itemData.field.floorCount; i1++)
            for (int i2 = 0; i2 < placementSystem.floor[0].transform.childCount; i2++)
                if (placementSystem.floorBool[i1, i2])
                    if (secondObject == null)
                    {
                        if (objectCount == placementSystem.floor[i1].transform.GetChild(i2).GetComponent<ObjectID>().childCount)
                            secondObject = placementSystem.floor[i1].transform.GetChild(i2).gameObject;
                    }
                    else if (objectCount == placementSystem.floor[i1].transform.GetChild(i2).GetComponent<ObjectID>().childCount)
                    {
                        thristhObject = placementSystem.floor[i1].transform.GetChild(i2).gameObject;
                        i2 = placementSystem.floor[0].transform.childCount;
                        i1 = itemData.field.floorCount;
                    }

        firstObject.GetComponent<ObjectTouch>().Touch();
        yield return new WaitForSeconds(0.31f);
        secondObject.GetComponent<ObjectTouch>().Touch();
        yield return new WaitForSeconds(0.31f);
        thristhObject.GetComponent<ObjectTouch>().Touch();
    }

}
