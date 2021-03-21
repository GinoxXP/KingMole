using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(LoadScene))]
    public class DefenseStrokeCounter : MonoBehaviour, StrokeCounter
    {
        [SerializeField] private float strokesLeft;
        private LoadScene _loadScene;

        private List<Enemy> _enemies = new List<Enemy>();

        private void Start()
        {
            _loadScene = GetComponent<LoadScene>();
        }

        public void Stroke()
        {
            strokesLeft--;

            StartCoroutine(CheckEnemies());

            if (strokesLeft < 0)
                StartCoroutine(GameOver());
        }

        IEnumerator GameOver()
        {
            float delayBeforeGameOver = 0.2f;
            yield return new WaitForSeconds(delayBeforeGameOver);
            
            _loadScene.Reload();
            yield return null;
        }

        IEnumerator CheckEnemies()
        {
            float delayCheck = 0.2f;
            yield return new WaitForSeconds(delayCheck);
            
            if(AllEnemiesAreDefeated())
                _loadScene.Load();
            
            yield return null;
        }

        bool AllEnemiesAreDefeated()
        {
            foreach (var enemy in _enemies)
            {
                if (enemy != null)
                    return false;
            }

            return true;
        }

        public void AddEnemy(Enemy enemy)
        {
            _enemies.Add(enemy);
        }
    }
}