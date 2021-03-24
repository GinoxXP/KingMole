using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 _target;
    private Vector3 _moveDirection;
    
    [SerializeField] private DetectZone upZone;
    [SerializeField] private DetectZone leftZone;
    [SerializeField] private DetectZone downZone;
    [SerializeField] private DetectZone rightZone;
    
    private bool _isRunning;
    
    private SpriteRenderer _spriteRenderer;

    private StrokeCounter _strokeCounter;
    
    [HideInInspector]
    public bool isCanWalk;
    
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _strokeCounter = GameObject.Find("Stroke Counter").GetComponent<StrokeCounter>();

        StartCoroutine(PauseBeforePlay());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(!_isRunning && context.performed && isCanWalk)
        {
            _moveDirection = context.ReadValue<Vector2>();

            if (_moveDirection.x != 0 && _moveDirection.y != 0)
                return;
            
            if(CheckFreeWay())
            {
                _isRunning = true;
                SetTarget();
                StartCoroutine(Move());
            }
        }
    }
    
    void SetTarget()
    {
        _target = transform.position + _moveDirection;
    }

    IEnumerator PauseBeforePlay()
    {
        yield return new WaitForSeconds(0.2f);
        isCanWalk = true;
        yield return null;
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
        _isRunning = false;
    }

    bool CheckFreeWay()
    {
        List<GameObject> detectedObjects = null;

        if(_moveDirection.x > 0)
        {
            detectedObjects = rightZone.detectedObjects;
            _spriteRenderer.flipX = true;
        }
        if(_moveDirection.x < 0)
        {
            detectedObjects = leftZone.detectedObjects;
            _spriteRenderer.flipX = false;
        }

        if(_moveDirection.y > 0)
            detectedObjects = upZone.detectedObjects;
        if(_moveDirection.y < 0)
            detectedObjects = downZone.detectedObjects;


        bool isFreeWay = true;
        
        foreach (var detectedObject in detectedObjects)
        {
            if(detectedObject == null || detectedObject.TryGetComponent(out SokobanZone _))
            {
                _strokeCounter.Stroke();
                if(isFreeWay)
                    isFreeWay = true;
            }
            else
            {
                isFreeWay = false;
                if (detectedObject.TryGetComponent(out Chest chest))
                {
                    chest.SetMoveDirection(_moveDirection);
                    _strokeCounter.Stroke();
                }

                if (detectedObject.TryGetComponent(out Enemy enemy))
                {
                    enemy.SetMoveDirection(_moveDirection);
                    _strokeCounter.Stroke();
                }
            }
        }
        
        if(isFreeWay)
            _strokeCounter.Stroke();

        return isFreeWay;
    }
}
