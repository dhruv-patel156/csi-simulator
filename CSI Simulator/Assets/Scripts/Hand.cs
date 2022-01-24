using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hand : MonoBehaviour
{

    Animator animator;
    private float gripTarget;
    private float gripCurrent;
    private float pointTarget;
    private float pointCurrent;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
    }

    internal void SetGrip(float v)
    {
        gripTarget = v;
    }

    internal void SetPoint(float v)
    {
        pointTarget = v;
    }

    void AnimateHand()
    {
        if (gripCurrent != gripTarget) {
            gripCurrent = Mathf.MoveTowards(gripCurrent, gripTarget, Time.deltaTime * speed);
            animator.SetFloat("Grip", gripCurrent);
        }

        if (pointCurrent != pointTarget) {
            pointCurrent = Mathf.MoveTowards(pointCurrent, pointTarget, Time.deltaTime * speed);
            animator.SetFloat("Point", pointCurrent);
        }
    }
}
