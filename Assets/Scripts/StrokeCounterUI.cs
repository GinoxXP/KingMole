using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.UI;

public class StrokeCounterUI : MonoBehaviour
{
    [SerializeField] private Text text;
    private DefenseStrokeCounter _counter;

    void Start()
    {
        if (!GameObject.Find("Stroke Counter").TryGetComponent(out _counter))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_counter.StokesLeft > 0)
            text.text = _counter.StokesLeft.ToString();
        else
            text.text = "X";
    }
}