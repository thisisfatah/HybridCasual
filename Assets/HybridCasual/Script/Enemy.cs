using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float MoveStep = 2.0f;

    Vector3 startPosition;
    [SerializeField] Animator animator;

    [SerializeField] float health = 1.0f;


    private void Awake()
    {
        startPosition = transform.position;
    }

    public void MoveStepForward()
    {
        startPosition.z -= MoveStep;
        animator.SetBool("IsWalking", true);
        transform.DOMoveZ(startPosition.z, 1.0f).OnComplete(() =>
        {
            animator.SetBool("IsWalking", false);
        });
    }

    public void CanDamage() 
    {
        health -= 1.0f;

        if(health <= 0.0f)
        {
            GameManager.instance.RemoveEnemy(this);
            Destroy(gameObject);
        }
    }
}
