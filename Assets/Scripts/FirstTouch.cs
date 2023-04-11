using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTouch : MonoSingleton<FirstTouch>
{
    bool isTouch, tempIsTouch;
    [SerializeField] GameObject anim;

    private void FixedUpdate()
    {
        if (GameManager.Instance.gameStat == GameManager.GameStat.start)
            if (!isTouch)
                if (Input.touchCount > 0)
                {
                    tempIsTouch = true;
                    isTouch = true;
                    anim.SetActive(false);
                    PlayerPrefs.SetInt("isTouch", 1);
                }
    }

    public void StartTouch()
    {
        if (PlayerPrefs.HasKey("isTouch")) isTouch = true;
        else StartCoroutine(Check());
    }

    IEnumerator Check()
    {
        yield return new WaitForSeconds(3);
        if (!isTouch && !tempIsTouch) anim.SetActive(true);
    }
}
