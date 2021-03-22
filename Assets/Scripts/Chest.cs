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
        if(CheckFreeWay(moveDirection))
        {
            _isMoving = true;
            SetTarget(moveDirection);
            StartCoroutine(Move());
        }
    }
    
    bool CheckFreeWay(Vector2 moveDirection)
    {
        List<GameObject> detectedObjects = null;
    
        if(moveDirection.x > 0)
            detectedObjects = rightZone.detectedObjects;
        if(moveDirection.x < 0)
            detectedObjects = leftZone.detectedObjects;

        if(moveDirection.y > 0)
            detectedObjects = upZone.detectedObjects;
        if(moveDirection.y < 0)
            detectedObjects = downZone.detectedObjects;
    
    
        bool isFreeWay = true;
            
        foreach (var detectedObject in detectedObjects)
        {
            if(detectedObject == null || detectedObject.TryGetComponent(out SokobanZone _))
            {
                if(isFreeWay)
                    isFreeWay = true;
            }
            else
            {
                isFreeWay = false;
            }
        }
    
        return isFreeWay;
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
