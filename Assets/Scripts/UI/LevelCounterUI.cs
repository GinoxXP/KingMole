using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCounterUI : MonoBehaviour
{
    [SerializeField] private Text text;

    private void Start()
    {
        string sceneNumber = "?";
        try
        {
            sceneNumber = Int32.Parse(
                SceneManager.GetActiveScene().name.
                    Replace("SLevel", "").
                    Replace("DLevel", "")
            ).ToString();
        }
        catch (ArgumentException) 
        {}

        text.text = sceneNumber;
    }
}