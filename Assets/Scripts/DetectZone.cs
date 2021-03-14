using UnityEngine;

namespace DefaultNamespace
{
    public class DetectZone : MonoBehaviour
    {
        public GameObject detectedObject;
        public GameObject DetectedObject => detectedObject;

        void OnTriggerStay2D(Collider2D collider)
        {
            if(collider.gameObject.TryGetComponent(out Rigidbody2D rb))
                if(detectedObject != rb.gameObject)
                    detectedObject = rb.gameObject;
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            if(collider.gameObject.TryGetComponent(out Rigidbody2D rb))
                detectedObject = null;
        }
    }
}