using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] float speed;
    private Vector3 _target;

    [SerializeField] DetectZone upZone;
    [SerializeField] DetectZone leftZone;
    [SerializeField] DetectZone downZone;
    [SerializeField] DetectZone rightZone;

    private bool _isMoving;

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


        if(detectedObject == null || detectedObject.TryGetComponent(out SokobanZone _))
        {
            _isMoving = true;
            SetTarget(moveDirection);
            StartCoroutine(Move());
        }
    }

    void SetTarget(Vector3 moveDirection)
    {
        _target = transform.position + moveDirection;
    }

    IEnumerator Move()
    {
        while (Vector3.Distance(transform.position, _target) > float.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                _target,
                speed * Time.deltaTime);
            yield return null;
        }
        _isMoving = false;
    }
}
