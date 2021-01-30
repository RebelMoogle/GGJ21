using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public enum GameState
    {
        IntroScreen,
        MainMenu,
        StartFight,
        Fighting,
        QuickIntro,
        PauseMenu,
        InBetween,
        GameOver
    }

    public delegate void OnStateChangeHandler();
    public event OnStateChangeHandler OnStateChange;

    public GameState gameState;

    private int round;

    public void ChangeState(GameState state)
    {
        if (gameState != state) { gameState = state; }
        OnStateChange();
    }

    public int GetCurrentRound()
    {
        return round;
    }

    public void SetRound(int roundNum)
    {
        round = roundNum;
    }
}
