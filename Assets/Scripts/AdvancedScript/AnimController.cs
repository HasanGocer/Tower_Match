using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Animancer;

public class AnimController : MonoBehaviour
{
    [SerializeField] private AnimancerComponent character;
    [SerializeField] private AnimationClip walk, death, �dle;

    public void CallIdleAnim()
    {
        character.Play(�dle, 0.2f);
    }
    public void CallDeadAnim()
    {
        character.Play(death, 0.2f);
    }
    public void CallWalkAnim()
    {
        character.Play(walk, 0.2f);
    }
}
