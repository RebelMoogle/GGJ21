﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public GameObject menuPrefab;
    public GameObject creditsPrefab;

    [HideInInspector]
    SceneHandler sceneHandler;
    public Canvas canvas;
    
    void Start()
    {
        GameObject menu = Instantiate(menuPrefab, canvas.transform);

        sceneHandler = GetComponent<SceneHandler>();

        Button vsAIButton = menu.transform.GetChild(2).GetChild(0).GetComponent<Button>();
        vsAIButton.onClick.AddListener(delegate { StartGame(1); });
        Button vs2PButton = menu.transform.GetChild(2).GetChild(1).GetComponent<Button>();
        vs2PButton.onClick.AddListener(delegate { StartGame(2); });
        Button creditsButton = menu.transform.GetChild(2).GetChild(2).GetComponent<Button>(); // I'm hardcoding the button values because I don't expect our prefab to change (much)
        creditsButton.onClick.AddListener(ShowCredits);
        Button exitButton = menu.transform.GetChild(2).GetChild(3).GetComponent<Button>(); // If we change the buttons in the menu around these references need to be rewritten
        exitButton.onClick.AddListener(ExitGame);
    }

    public void StartGame(int scene)
    {
            sceneHandler.LoadScene(scene);
    }

    public void ShowCredits()
    {
        GameObject credits = Instantiate(creditsPrefab, canvas.transform);
    }

    public void ExitGame()
    {
        Application.Quit();
    }


}
