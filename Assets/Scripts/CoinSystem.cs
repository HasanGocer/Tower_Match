using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CoinSystem : MonoSingleton<CoinSystem>
{
    [SerializeField] int _OPCoinCount;
    [SerializeField] int _coinCount;
    [SerializeField] GameObject _parent, _StartPos, _finishPos;

    public IEnumerator CoinMove()
    {
        List<GameObject> coins = new List<GameObject>();

        for (int i = 0; i < _coinCount; i++)
            coins.Add(ObjectPool.Instance.GetPooledObject(_OPCoinCount, _StartPos.transform.position, _parent.transform));

        foreach (GameObject item in coins)
        {
            item.transform.DOJump(_finishPos.transform.position, Random.Range(0, 10) / 10, Random.Range(0, 10) / 10, 1f);
            MoneySystem.Instance.MoneyTextRevork(Random.Range(2, 8));
            yield return new WaitForSeconds(0.1f);
        }
        yield return new WaitForSeconds(1);
    }
}
