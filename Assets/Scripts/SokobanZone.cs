using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class SokobanZone : MonoBehaviour
    {
        private SokobanStrokeCounter _sokobanStrokeCounter;
        
        private bool _isFull;
        public bool IsFull => _isFull;

        private void Start()
        {
            _sokobanStrokeCounter = GameObject.Find("Sokoban Stroke Counter").GetComponent<SokobanStrokeCounter>();
            _sokobanStrokeCounter.AddSokobanZone(this);
        }

        void OnTriggerStay2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out Chest chest))
                _isFull = true;
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            if (collider.gameObject.TryGetComponent(out Chest chest))
                _isFull = false;
        }
    }
}