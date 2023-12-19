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
    Collider coll;

    private void Awake()
    {
        startPosition = transform.position;
        coll = GetComponent<Collider>();
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish")
        {
            GameManager.instance.GameOver();
        }
    }
}
