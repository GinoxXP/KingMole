using DefaultNamespace;
using UnityEngine;

public class Curtain : MonoBehaviour
{
    [HideInInspector]
    public DefaultNamespace.LoadScene loadScene;
    
    private Animator _animator;

    public delegate void LoadSceneDelegate();
    private LoadSceneDelegate Load;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    
    public void NextLevel()
    {
        _animator.Play("CurtainSuccessfulLevelAnimation");
        Load = loadScene.Load;
    }

    public void ReloadLevel()
    {
        _animator.Play("CurtainFailedLevelAnimation");
        Load = loadScene.Reload;
    }

    public void ExecuteLoad()
    {
        Load();
    }
}