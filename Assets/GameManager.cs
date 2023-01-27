using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    void Awake() { instance = this; }

    [SerializeField] GameObject _oceanSurface;
    [HideInInspector] public GameObject oceanSurface { get; private set;}

    private void Update()
    {
        oceanSurface = _oceanSurface;
    }
}
