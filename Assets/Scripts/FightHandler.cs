using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FightHandler : MonoBehaviour
{
    [HideInInspector]
    public int round;
    public GameObject playerOnePrefab;
    private ReceiveDamage playerOneDamage;
    public healthBar playerOneHP;
    public GameObject playerTwoPrefab;
    private ReceiveDamage playerTwoDamage;
    public healthBar playerTwoHP;
    [HideInInspector]
    public int playerOneState;
    [HideInInspector]
    public int playerTwoState;
    private bool roundEnded;
    //TODO: Make a 3 2 1 go prefab
    //TODO: Make a Player Win/Loss prefab

    [HideInInspector]
    public DialogHandler dialogHandler;
    [HideInInspector]
    SceneHandler sceneHandler;

    public bool VsAI;

    public UnityEvent roundOneStartEvents;
    public UnityEvent roundTwoStartEvents;
    public UnityEvent roundThreeStartEvents;

    void Start()
    {
        dialogHandler = GetComponent<DialogHandler>();
        sceneHandler = GetComponent<SceneHandler>();
        SetupRound();
        //Debug.Log("This is round " + PlayerPrefs.GetInt("Round"));
        //Debug.Log("Player one has lost " + PlayerPrefs.GetInt("PlayerOneState") + " round(s)");
        //Debug.Log("Player two has lost " + PlayerPrefs.GetInt("PlayerTwoState") + " round(s)");
        //Debug.Log("The round has ended is " + roundEnded);
    }

    void FixedUpdate()
    {
        //manually checking each player's health due to some confusion
        // TODO: Edit the player so they can tell the FightHandler when they are knocked out via a function call so the game isn't checking every frame
        if(playerOneDamage.CurrentHealth == 0)
        {
            if (!roundEnded)
            {
                EndRound(1);
                roundEnded = true;
            }
            
        }
        if (playerTwoDamage.CurrentHealth == 0)
        {
            if (!roundEnded)
            {
                EndRound(2);
                roundEnded = true;
            }
        }
        //Debug.Log("Player One health is " + playerOneDamage.CurrentHealth);
        //Debug.Log("Player Two health is " + playerTwoDamage.CurrentHealth);

    }

    void SetRound()
    {
        round = PlayerPrefs.GetInt("Round", 1); //automatically sets round to 1 if first time playing
    }

    void SetPlayerStates()
    {
        playerOneState = PlayerPrefs.GetInt("PlayerOneState", 0);
        playerTwoState = PlayerPrefs.GetInt("PlayerTwoState", 0);
    }

    void SetupRound()
    {
        SetRound();
        SetPlayerStates();

        switch(round)
        {
            case 3:
                //Final round
                SpawnPlayers();
                roundThreeStartEvents.Invoke();
                break;
            case 2:
                //2nd round
                SpawnPlayers();
                roundTwoStartEvents.Invoke();
                break;
            default:
                //1st round
                dialogHandler.DialogStart();
                SpawnPlayers();
                roundOneStartEvents.Invoke();
                break;
        }
    }

    void SpawnPlayers()
    {
        GameObject playerOne = Instantiate(playerOnePrefab);
        playerOneDamage = playerOne.GetComponent<ReceiveDamage>();
        playerOneHP.player = playerOne;
        GameObject playerTwo = Instantiate(playerTwoPrefab);
        playerTwoDamage = playerTwo.GetComponent<ReceiveDamage>();
        playerTwoHP.player = playerTwo;
    }

    public void EndRound(int loser)
    {
        incRound();

        if(loser == 1)
        {
            if (playerOneState == 1)
            {
                dialogHandler.DialogLostMatch();
                EndMatch();
            }
            if (playerOneState == 0)
            {
                PlayerPrefs.SetInt("PlayerOneState", 1);
                dialogHandler.DialogLostRound();
                StartCoroutine(NextRoundTimer());
            }
        }

        if (loser == 2)
        {
            if (playerTwoState == 1)
            {
                dialogHandler.DialogWonMatch();
                EndMatch();
            }
            if (playerTwoState == 0)
            {
                PlayerPrefs.SetInt("PlayerTwoState", 1);
                dialogHandler.DialogWinRound();
                StartCoroutine(NextRoundTimer());
            }
        }
    }

    void incRound()
    {
        switch (round)
        {
            case 3:
                //Final round
                EndMatch();
                break;
            case 2:
                //2nd round
                PlayerPrefs.SetInt("Round", 3);
                break;
            case 1:
                //1st round
                PlayerPrefs.SetInt("Round", 2);
                break;
            default:
                //1st round, doubled, just in case
                PlayerPrefs.SetInt("Round", 2);
                break;
        }
    }

    public void EndMatch()
    {
        PlayerPrefs.SetInt("Round", 1);
        PlayerPrefs.SetInt("PlayerOneState", 0);
        PlayerPrefs.SetInt("PlayerTwoState", 0);
        StartCoroutine(EndTimer());
    }

    IEnumerator NextRoundTimer()
    {
        yield return new WaitForSeconds(2.5f);
        if (VsAI)
        {
            sceneHandler.LoadScene(1);
        }
        if (!VsAI)
        {
            sceneHandler.LoadScene(2);
        }

    }

    IEnumerator EndTimer()
    {
        yield return new WaitForSeconds(2.5f);
        
        sceneHandler.LoadScene(0);
    }








}
