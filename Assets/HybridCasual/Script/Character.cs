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

    List<Arrow> arrowList = new List<Arrow>();


    [Header("Character Weapon Setting")]
    [SerializeField] GameObject AimRef;
    [SerializeField] GameObject ArrowReference;
    [SerializeField] SplineContainer splineContainer;
    [SerializeField] GameObject[] AimLocation;
    [SerializeField] int arrowCount = 2;
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
        spline.SetKnot(1, new BezierKnot(new Unity.Mathematics.float3(AimLocation[0].transform.position.x, 0, 15)));
        spline.SetKnot(2, new BezierKnot(new Unity.Mathematics.float3(AimLocation[1].transform.position.x, 0, 30)));
        spline.SetKnot(3, new BezierKnot(new Unity.Mathematics.float3(AimLocation[2].transform.position.x, 0, 45)));
        spline.SetKnot(4, new BezierKnot(new Unity.Mathematics.float3(AimLocation[3].transform.position.x, 0, 60)));
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
        if(!Moving)
        {
            spline.SetKnot(1, new BezierKnot(new Unity.Mathematics.float3(-10, 0, 10)));
            animator.SetTrigger("IsShooting");

            GameManager.instance.ShowUIShoot(false);
            StartCoroutine(ShootDelay());
        }
    }
    IEnumerator ShootDelay()
    {
        for (int i = 0; i < arrowCount; i++)
        {
            GameObject arrowSpawn = Instantiate(ArrowReference, transform, true);
            Arrow arrow = arrowSpawn.GetComponent<Arrow>();
            arrow.SetStartMove(splineContainer);
            arrowList.Add(arrow);
            yield return new WaitForSeconds(1.0f);
        }
    }

    public void RemoveArrow(Arrow arrow)
    {
        arrowList.Remove(arrow);

        if(arrowList.Count <= 0)
        {
            GameManager.instance.ShowUIShoot(true);
            GameManager.instance.SpawnNewEnemy();
            arrowList.Clear();
        }
    }
}
