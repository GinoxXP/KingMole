using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 target;
    private Vector3 moveDirection;
    
    [SerializeField] private DetectZone upZone;
    [SerializeField] private DetectZone leftZone;
    [SerializeField] private DetectZone downZone;
    [SerializeField] private DetectZone rightZone;
    
    private bool isRunning;
    
    private SpriteRenderer spriteRenderer;
    
    [HideInInspector]
    public bool isCanWalk;
    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        StartCoroutine(PauseBeforePlay());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if(!isRunning && context.performed && isCanWalk)
        {
            moveDirection = context.ReadValue<Vector2>();

            if (moveDirection.x != 0 && moveDirection.y != 0)
                return;
            
            if(CheckFreeWay())
            {
                isRunning = true;
                SetTarget();
                StartCoroutine(Move());
            }
        }
    }
    
    void SetTarget()
    {
        target = transform.position + moveDirection;
    }

    IEnumerator PauseBeforePlay()
    {
        yield return new WaitForSeconds(0.2f);
        isCanWalk = true;
        yield return null;
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
        isRunning = false;
    }

    bool CheckFreeWay()
    {
        GameObject detectedObject = null;

        if(moveDirection.x > 0)
        {
            detectedObject = rightZone.DetectedObject;
            spriteRenderer.flipX = true;
        }
        if(moveDirection.x < 0)
        {
            detectedObject = leftZone.DetectedObject;
            spriteRenderer.flipX = false;
        }

        if(moveDirection.y > 0)
            detectedObject = upZone.DetectedObject;
        if(moveDirection.y < 0)
            detectedObject = downZone.DetectedObject;


        if(detectedObject == null)
        {
            //TODO stroke
            return true;
        }
        else
        {
            if (detectedObject.TryGetComponent(out Chest chest))
            {
                chest.SetMoveDirection(moveDirection);
            }
        }
        
        return false;
    }
}
