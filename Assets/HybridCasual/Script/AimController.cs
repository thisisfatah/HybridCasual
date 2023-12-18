using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class AimController : MonoBehaviour
{
    [SerializeField] float speed;
    float distancePercentage;
    float splineLength;
    SplineContainer spline;


    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        if (spline != null)
        {
            distancePercentage += speed * Time.deltaTime / splineLength;
            Vector3 currentPosition = spline.EvaluatePosition(distancePercentage);
            transform.position = currentPosition;

            Vector3 nexposition = spline.EvaluatePosition(distancePercentage + 0.05f);
            Vector3 direction = nexposition - currentPosition;
            transform.rotation = Quaternion.LookRotation(direction, transform.up);
            if (distancePercentage > 1)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetStartMove(SplineContainer newSpline)
    {
        spline = newSpline;
        splineLength = spline.CalculateLength();
    }
}
