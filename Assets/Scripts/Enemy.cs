using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(Animator), typeof(BoxCollider2D))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] float speed;
        private Vector3 _target;

        [SerializeField] DetectZone upZone;
        [SerializeField] DetectZone leftZone;
        [SerializeField] DetectZone downZone;
        [SerializeField] DetectZone rightZone;

        private bool _isMoving;

        private Animator _animator;
        private BoxCollider2D _collider;

        private DefenseStrokeCounter _strokeCounter;

        void Start()
        {
            _animator = GetComponent<Animator>();
            _collider = GetComponent<BoxCollider2D>();
            _strokeCounter = GameObject.Find("Stroke Counter").GetComponent<DefenseStrokeCounter>();
            _strokeCounter.AddEnemy(this);
        }

        public void SetMoveDirection(Vector2 moveDirection)
        {
            if(CheckFreeWay(moveDirection))
            {
                _animator.Play("DamageAnimation");
                _isMoving = true;
                SetTarget(moveDirection);
                StartCoroutine(Move());
            }
            else
            {
                _collider.enabled = false;
                _animator.Play("DeathAnimation");
            }
        }

        public void Death()
        {
          Destroy(gameObject);
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
}
