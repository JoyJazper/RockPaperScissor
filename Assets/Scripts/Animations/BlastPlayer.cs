using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlastPlayer : MonoBehaviour
{
    [SerializeField] private Animator animator;

    public void PlayAnimation(BlastsAnimations blast)
    {
        switch(blast)
        {
            case BlastsAnimations.Blast1:
                animator.Play("Blast1");
                break;

            case BlastsAnimations.Blast2:
                animator.Play("Blast2");
                break;

            case BlastsAnimations.Blast3:
                animator.Play("Blast3");
                break;
        }
    }
}

public enum BlastsAnimations
{
    Blast1,
    Blast2,
    Blast3
}
