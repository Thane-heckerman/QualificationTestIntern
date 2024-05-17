
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class CoinAnimation : MonoBehaviour, IAnimationClip
{
    public AnimationClip coinAnim;
    private Animator animator;
    public AnimationClip Animation
    {
        get => coinAnim
    ; set => coinAnim = value;
    }

    private void Awake()
    {
    }
    public void Setup(Animator _animator)
    {
        animator = _animator;
    }
    public void Play()
    {
        animator.Play(coinAnim.name);
    }

}
