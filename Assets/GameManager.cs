using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    public static GameManager instance;
    void Awake() { instance = this; }

    [SerializeField] GameObject _oceanSurface;
    [HideInInspector] public GameObject oceanSurface { get; private set;}

    [SerializeField] TextMeshProUGUI instructionPrompt;
    public GameObject player;
    GameObject promptSource;

    private void Start()
    {
        instructionPrompt.gameObject.SetActive(false);
    }

    private void Update()
    {
        oceanSurface = _oceanSurface;
    }

    public void DisplayPrompt(string prompt, GameObject source)
    {
        if (instructionPrompt.text == prompt && instructionPrompt.gameObject.activeInHierarchy) return;

        promptSource = source;
        instructionPrompt.text = prompt;
        instructionPrompt.gameObject.SetActive(true);
    }
    public void HidePrompt(string text, GameObject source, bool _override = false)
    {
        if (instructionPrompt.text != text || (promptSource != source && !_override) ) return;

        promptSource = null;
        instructionPrompt.text = "";
        instructionPrompt.gameObject.SetActive(false);
    }

}
