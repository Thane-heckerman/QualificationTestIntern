using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class CoinAnimManager : MonoBehaviour
{
    public static CoinAnimManager Instance { get; private set; }
    public float time;
    List<Transform> animations = new List<Transform>();
    private Animator animator;
    public void PlayAnim(string animationName, Transform OtherObjectA, Transform OtherObjectB, float time = 0f)
    {
        Transform animationObj = animations.Where(a => a.name == animationName).FirstOrDefault();
        Vector3 position = Vector3.Lerp(OtherObjectA.position, OtherObjectB.position, 0.01f);
        Vector2 pos = FindMidPoinBetweenVectors(OtherObjectA, OtherObjectB);
        transform.position = position;
        // animator.Play("StarCoinRotateAndAppear");
        transform.DOMove(transform.position + Vector3.up, .5f);
        transform.DOScale(new Vector2(.05f, .05f), .5f).OnComplete(() => { transform.localScale = Vector2.zero; });
        // animationObj.GetComponent<CoinAnimation>().Play();
    }
    public Vector2 FindMidPoinBetweenVectors(Transform OtherObjectA, Transform OtherObjectB)
    {
        Vector3 directionCtoA = OtherObjectA.position - transform.position;
        Vector3 directionCtoB = OtherObjectB.position - transform.position;
        Vector3 directionAtoB = OtherObjectB.position - OtherObjectA.position;
        Vector3 midpointAtoB = new Vector2((directionCtoA.x + directionCtoB.x) / 2.0f, (directionCtoA.y + directionCtoB.y) / 2.0f);

        return midpointAtoB;
    }
    private void Awake()
    {
        Instance = this;
        animator = GetComponent<Animator>();
        transform.localScale = Vector2.zero;
    }

    private void Start()
    {
        GetAnims();
    }

    private void GetAnims()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<IAnimationClip>().Setup(animator);
            animations.Add(transform.GetChild(i));

        }
        Debug.Log("Animations count " + animations.Count);
    }
}
