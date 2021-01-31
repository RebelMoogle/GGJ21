using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        MainMenu,
        StartFight,
        Fighting,
        EndFight,
        PauseMenu,
        GameOver
    }

    public delegate void OnStateChangeHandler();
    public event OnStateChangeHandler OnStateChange;

    [Header("Game State Settings")]
    public GameState gameState;
    private int round;

    [Space]
    [Header("UI")]
    public GameObject pauseMenuPrefab;

    public GameObject optionsMenuPrefab;

    private GameObject pauseMenu;
    private GameObject optionsMenu;

    public void ChangeState(GameState state)
    {
        if (gameState != state) { gameState = state; }
        //OnStateChange();
    }

    public int GetCurrentRound()
    {
        return round;
    }

    public void SetRound(int roundNum)
    {
        round = roundNum;
    }

    public void GetPauseMenu()
    {
        ChangeState(GameState.PauseMenu);
        try
        {
            if(pauseMenu == null)
            {
                pauseMenu = Instantiate(pauseMenuPrefab);
            }
            else
            {
                pauseMenu.SetActive(true);
            }
        }
        catch(System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }

    }

    //creates an instance of the options menu prefab
    public void GetOptionsMenu(){
        try
        {
            if(optionsMenu == null)
            {
                optionsMenu = Instantiate(optionsMenuPrefab);
            }else
            {
                optionsMenu.SetActive(true);
            }
        }
        catch(System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
}
