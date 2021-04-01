using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [RequireComponent(typeof(LoadScene))]
    public class DefenseStrokeCounter : MonoBehaviour, StrokeCounter
    {
        [SerializeField] private int strokesLeft;
        public int StokesLeft => strokesLeft;
        
        private LoadScene _loadScene;

        private List<Enemy> _enemies = new List<Enemy>();

        private PlayerController _playerController;

        private Curtain _curtain;

        private void Start()
        {
            _loadScene = GetComponent<LoadScene>();

            _playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

            _curtain = GameObject.Find("Curtain").GetComponent<Curtain>();
            _curtain.loadScene = _loadScene;
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
            _playerController.isCanWalk = false;
            
            float delayBeforeGameOver = 0.2f;
            yield return new WaitForSeconds(delayBeforeGameOver);
            
            _curtain.ReloadLevel();
            yield return null;
        }

        IEnumerator CheckEnemies()
        {
            float delayCheck = 0.2f;
            yield return new WaitForSeconds(delayCheck);

            if (AllEnemiesAreDefeated())
            {
                _playerController.isCanWalk = false;
                _curtain.NextLevel();
            }

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