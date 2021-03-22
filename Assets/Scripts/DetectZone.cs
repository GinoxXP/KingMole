using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class DetectZone : MonoBehaviour
    {
        public List<GameObject> detectedObjects;

        void OnTriggerEnter2D(Collider2D collider)
        {
            if(collider.gameObject.TryGetComponent(out Rigidbody2D rb))
                detectedObjects.Add(rb.gameObject);
        }

        void OnTriggerExit2D(Collider2D collider)
        {
            if(collider.gameObject.TryGetComponent(out Rigidbody2D rb))
                detectedObjects.Remove(rb.gameObject);
        }
    }
}