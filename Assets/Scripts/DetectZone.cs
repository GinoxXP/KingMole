using UnityEngine;

namespace DefaultNamespace
{
    public class DetectZone : MonoBehaviour
    {
        private GameObject _detectedObject;
        public GameObject DetectedObject => _detectedObject;

        void OnTriggerStay2D(Collider2D collider)
        {
            if(collider.gameObject.TryGetComponent(out Rigidbody2D rb))
                if(_detectedObject != rb.gameObject)
                    _detectedObject = rb.gameObject;
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            if(collider.gameObject.TryGetComponent(out Rigidbody2D rb))
                _detectedObject = null;
        }
    }
}