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
        _perButton.onClick.AddListener(PerTime);
        _perCountText.text = GameManager.Instance.perTime.ToString();
    }

    private void PerTime()
    {
        if (GameManager.Instance.perTime > 0)
        {
            PlacementSystem placementSystem = PlacementSystem.Instance;
            ItemData itemData = ItemData.Instance;
            int objectCount = 0;

            GameManager.Instance.SetPerTime();
            _perCountText.text = GameManager.Instance.perTime.ToString();

            for (int i1 = 0; i1 < itemData.field.floorCount; i1++)
                for (int i2 = 0; i2 < placementSystem.floor[0].transform.childCount; i2++)
                    if (placementSystem.floorBool[i1, i2])
                    {
                        firstObject = placementSystem.apartment[i1, i2].gameObject;
                        objectCount = placementSystem.apartment[i1, i2].GetComponent<ObjectID>().childCount;
                        i2 = placementSystem.floor[0].transform.childCount;
                        i1 = itemData.field.floorCount;
                    }
            for (int i1 = 0; i1 < itemData.field.floorCount; i1++)
                for (int i2 = 0; i2 < placementSystem.floor[0].transform.childCount; i2++)
                    if (placementSystem.floorBool[i1, i2])
                        if (secondObject == null && placementSystem.apartment[i1, i2] != firstObject)
                        {
                            if (objectCount == placementSystem.apartment[i1, i2].GetComponent<ObjectID>().childCount)
                                secondObject = placementSystem.apartment[i1, i2].gameObject;
                        }
                        else if (objectCount == placementSystem.apartment[i1, i2].GetComponent<ObjectID>().childCount && placementSystem.apartment[i1, i2] != firstObject)
                        {
                            thristhObject = placementSystem.apartment[i1, i2].gameObject;
                            i2 = placementSystem.floor[0].transform.childCount;
                            i1 = itemData.field.floorCount;
                        }

            if (objectCount != ObjectManager.Instance.tempObjectCount)
                ObjectManager.Instance.WrongItem(true);
            firstObject.GetComponent<ObjectTouch>().Touch();
            secondObject.GetComponent<ObjectTouch>().Touch();
            thristhObject.GetComponent<ObjectTouch>().Touch();

            firstObject = null; secondObject = null; thristhObject = null;
        }
    }

}
