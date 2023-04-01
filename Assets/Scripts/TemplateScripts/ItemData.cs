using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoSingleton<ItemData>
{
    [System.Serializable]
    public class Field
    {
        public int sizeCount, floorCount;
    }

    public Field field;
    public Field standart;
    public Field factor;
    public Field constant;
    public Field maxFactor;
    public Field max;
    public Field fieldPrice;

    public void AwakeID()
    {
        field.sizeCount = standart.sizeCount + (factor.sizeCount * constant.sizeCount);
        fieldPrice.sizeCount = fieldPrice.sizeCount * factor.sizeCount;
        field.floorCount = standart.floorCount + (factor.floorCount * constant.floorCount);
        fieldPrice.floorCount = fieldPrice.floorCount * factor.floorCount;


        /*
         field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        */
        if (factor.sizeCount > maxFactor.sizeCount)
        {
            factor.sizeCount = maxFactor.sizeCount;
            field.sizeCount = standart.sizeCount + (factor.sizeCount * constant.sizeCount);
            fieldPrice.sizeCount = fieldPrice.sizeCount / (factor.sizeCount - 1);
            fieldPrice.sizeCount = fieldPrice.sizeCount * factor.sizeCount;
        }
        if (factor.floorCount > maxFactor.floorCount)
        {
            factor.floorCount = maxFactor.floorCount;
            field.floorCount = standart.floorCount + (factor.floorCount * constant.floorCount);
            fieldPrice.floorCount = fieldPrice.floorCount / (factor.floorCount - 1);
            fieldPrice.floorCount = fieldPrice.floorCount * factor.floorCount;
        }


        /*
          if (factor.objectCount > maxFactor.objectCount)
        {
            factor.objectCount = maxFactor.objectCount;
            field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
            fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
            fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        }
        */

        StartCoroutine(Buttons.Instance.LoadingScreen());
    }

    public void SetSizeCount()
    {
        factor.sizeCount++;

        field.sizeCount = standart.sizeCount + (factor.sizeCount * constant.sizeCount);
        fieldPrice.sizeCount = fieldPrice.sizeCount / (factor.sizeCount - 1);
        fieldPrice.sizeCount = fieldPrice.sizeCount * factor.sizeCount;

        if (factor.sizeCount > maxFactor.sizeCount)
        {
            factor.sizeCount = maxFactor.sizeCount;
            field.sizeCount = standart.sizeCount + (factor.sizeCount * constant.sizeCount);
            fieldPrice.sizeCount = fieldPrice.sizeCount / (factor.sizeCount - 1);
            fieldPrice.sizeCount = fieldPrice.sizeCount * factor.sizeCount;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    public void SetFloorCount()
    {
        factor.floorCount++;

        field.floorCount = standart.floorCount + (factor.floorCount * constant.floorCount);
        fieldPrice.floorCount = fieldPrice.floorCount / (factor.floorCount - 1);
        fieldPrice.floorCount = fieldPrice.floorCount * factor.floorCount;

        if (factor.floorCount > maxFactor.floorCount)
        {
            factor.floorCount = maxFactor.floorCount;
            field.floorCount = standart.floorCount + (factor.floorCount * constant.floorCount);
            fieldPrice.floorCount = fieldPrice.floorCount / (factor.floorCount - 1);
            fieldPrice.floorCount = fieldPrice.floorCount * factor.floorCount;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }


    /*
     public void SetObjectCount()
    {
        factor.objectCount++;

        field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
        fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
        fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;

        if (factor.objectCount > maxFactor.objectCount)
        {
            factor.objectCount = maxFactor.objectCount;
            field.objectCount = standart.objectCount + (factor.objectCount * constant.objectCount);
            fieldPrice.objectCount = fieldPrice.objectCount / (factor.objectCount - 1);
            fieldPrice.objectCount = fieldPrice.objectCount * factor.objectCount;
        }

        GameManager.Instance.FactorPlacementWrite(factor);
    }
    */
}
