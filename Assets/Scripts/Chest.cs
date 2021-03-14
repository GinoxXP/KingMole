using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] float speed;
    private Vector3 target;

    [SerializeField] DetectZone upZone;
    [SerializeField] DetectZone leftZone;
    [SerializeField] DetectZone downZone;
    [SerializeField] DetectZone rightZone;

    private bool isMoving;

    public void SetMoveDirection(Vector3 moveDirection)
    {
        GameObject detectedObject = null;

        if(moveDirection.x > 0)
            detectedObject = rightZone.DetectedObject;
        if(moveDirection.x < 0)
            detectedObject = leftZone.DetectedObject;

        if(moveDirection.y > 0)
            detectedObject = upZone.DetectedObject;
        if(moveDirection.y < 0)
            detectedObject = downZone.DetectedObject;


        if(detectedObject == null)
        {
            isMoving = true;
            SetTarget(moveDirection);
            StartCoroutine(Move());
        }
    }

    void SetTarget(Vector3 moveDirection)
    {
        target = transform.position + moveDirection;
    }

    IEnumerator Move()
    {
        while (Vector3.Distance(transform.position, target) > float.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                target,
                speed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
    }
}
