using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(LoadScene))]
    public class SokobanStrokeCounter : MonoBehaviour, StrokeCounter
    {
        private List<SokobanZone> sokobanZones = new List<SokobanZone>();
        private LoadScene _loadScene;

        private void Start()
        {
            _loadScene = GetComponent<LoadScene>();
        }

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
                _loadScene.Load();

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