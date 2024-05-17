using UnityEngine;

public interface IAnimationClip
{

    public AnimationClip Animation { get; set; }
    public void Play();
    public void Setup(Animator _animator);
}
