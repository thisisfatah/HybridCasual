using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Splines;
using System;
using System.Linq;

public class Character : MonoBehaviour
{
    [Header("Character Movement Setting")]
    [SerializeField] float MoveStep = 2f;
    Vector3 startPosition;
    Animator animator;
    bool Moving;


    [Header("Character Weapon Setting")]
    [SerializeField] GameObject AimRef;
    [SerializeField] GameObject ArrowReference;
    [SerializeField] SplineContainer splineContainer;
    [SerializeField] GameObject[] AimLocation;
    Spline spline;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startPosition = transform.position;
        spline = splineContainer.Splines[0];
    }

    // Update is called once per frame
    void Update()
    {
        spline.SetKnot(0, new BezierKnot(new Unity.Mathematics.float3(transform.position.x, 0, 0)));
        spline.SetKnot(1, new BezierKnot(new Unity.Mathematics.float3(AimLocation[0].transform.position.x, 0, 25)));
        spline.SetKnot(2, new BezierKnot(new Unity.Mathematics.float3(AimLocation[1].transform.position.x, 0, 50)));
        spline.SetKnot(3, new BezierKnot(new Unity.Mathematics.float3(AimLocation[2].transform.position.x, 0, 75)));
    }

    public void SetCharacterMoveRight()
    {
        if (!Moving)
        {
            Moving = true;
            AimRef.SetActive(false);
            for(int i = 0; i < AimLocation.Length; i++)
            {
                AimLocation[i].SetActive(false);
            }
            transform.DORotate(new Vector3(0, 90, 0), 0.3f).OnComplete(() =>
            {
                if (transform.position.x < 14)
                {
                    startPosition.x += MoveStep;
                    animator.SetBool("IsWalking", true);
                    transform.DOMoveX(startPosition.x, 1).OnComplete(() =>
                    {
                        animator.SetBool("IsWalking", false);
                        transform.DORotate(new Vector3(0, 0, 0), 0.3f).OnComplete(() =>
                        {
                            AimRef.SetActive(true);
                            Moving = false;
                            for (int i = 0; i < AimLocation.Length; i++)
                            {
                                AimLocation[i].SetActive(true);
                            }
                        });
                    });
                }
                else
                {
                        transform.DORotate(new Vector3(0, 0, 0), 0.3f).OnComplete(() => 
                        {
                            for (int i = 0; i < AimLocation.Length; i++)
                            {
                                AimLocation[i].SetActive(true);
                            }
                            AimRef.SetActive(true); 
                            Moving = false; 
                        });
                }
            });
        }
    }

    public void SetCharacterMoveLeft()
    {
        if(!Moving)
        {
            Moving = true;
            AimRef.SetActive(false);
            for (int i = 0; i < AimLocation.Length; i++)
            {
                AimLocation[i].SetActive(false);
            }
            transform.DORotate(new Vector3(0, -90, 0), 0.3f).OnComplete(() =>
            {
                if (transform.position.x > -14)
                {
                    startPosition.x -= MoveStep;
                    animator.SetBool("IsWalking", true);
                    transform.DOMoveX(startPosition.x, 1).OnComplete(() =>
                    {
                        animator.SetBool("IsWalking", false);
                        transform.DORotate(new Vector3(0, 0, 0), 0.3f).OnComplete(() =>
                        {
                            AimRef.SetActive(true);
                            Moving = false;
                            for (int i = 0; i < AimLocation.Length; i++)
                            {
                                AimLocation[i].SetActive(true);
                            }
                        });
                    });
                }
                else
                {
                    transform.DORotate(new Vector3(0, 0, 0), 0.3f).OnComplete(() => 
                    { 
                        AimRef.SetActive(true); 
                        Moving = false;
                        for (int i = 0; i < AimLocation.Length; i++)
                        {
                            AimLocation[i].SetActive(true);
                        }
                    });
                }
            });
        }
    }

    public void Shooting()
    {
        if (!Moving)
        {
            spline.SetKnot(1, new BezierKnot(new Unity.Mathematics.float3(-10, 0, 10)));
            animator.SetTrigger("IsShooting");
            GameObject ArrowSpawn = Instantiate(ArrowReference, transform, true);
            AimController Arrow = ArrowSpawn.GetComponent<AimController>();
            Arrow.SetStartMove(splineContainer);
        }
    }
}
