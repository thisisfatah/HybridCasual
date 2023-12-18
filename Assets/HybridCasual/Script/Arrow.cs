using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Arrow : MonoBehaviour
{
    [SerializeField] float speed;
    float distancePercentage;
    float splineLength;
    SplineContainer spline;

    [SerializeField] Collider collision;

    private void Awake()
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
                GameManager.instance.character.RemoveArrow(this);
                Destroy(gameObject);
            }
        }
    }

    public void SetStartMove(SplineContainer newSpline)
    {
        spline = newSpline;
        splineLength = spline.CalculateLength();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy>().CanDamage();
            Debug.Log("Hit Enemy");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
