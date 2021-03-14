using System.Collections;
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

        _strokeCounter = GameObject.Find("Sokoban Stroke Counter").GetComponent<StrokeCounter>();

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
        GameObject detectedObject = null;

        if(_moveDirection.x > 0)
        {
            detectedObject = rightZone.DetectedObject;
            _spriteRenderer.flipX = true;
        }
        if(_moveDirection.x < 0)
        {
            detectedObject = leftZone.DetectedObject;
            _spriteRenderer.flipX = false;
        }

        if(_moveDirection.y > 0)
            detectedObject = upZone.DetectedObject;
        if(_moveDirection.y < 0)
            detectedObject = downZone.DetectedObject;


        if(detectedObject == null || detectedObject.TryGetComponent(out SokobanZone _))
        {
            _strokeCounter.Stroke();
            return true;
        }
        else
        {
            if (detectedObject.TryGetComponent(out Chest chest))
            {
                chest.SetMoveDirection(_moveDirection);
                _strokeCounter.Stroke();
            }
        }
        
        return false;
    }
}
