using UnityEngine;
using UnityEngine.SceneManagement;

namespace DefaultNamespace
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] string sceneName;

        public void Load()
        {
            SceneManager.LoadScene(sceneName);
        }

        public void Reload()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}