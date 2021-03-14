using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    public class SokobanStrokeCounter : MonoBehaviour, StrokeCounter
    {
        private List<SokobanZone> sokobanZones = new List<SokobanZone>();

        public void AddSokobanZone(SokobanZone sokobanZone)
        {
            sokobanZones.Add(sokobanZone);
        }
        
        public void Stroke()
        {
            StartCoroutine(CheckZones());
        }

        IEnumerator CheckZones()
        {
            yield return new WaitForSeconds(.2f);
            
            if(AllZonesIsFull())
                //TODO load next level
                Debug.Log("All zones is full");

            yield return null;
        }
        private bool AllZonesIsFull()
        {
            foreach (var sokobanZone in sokobanZones)
            {
                if (!sokobanZone.IsFull)
                    return false;
            }

            return true;
        }
    }
}