using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManagerRelay : MonoBehaviour
{
    [SerializeField]
    private GameManager gm;

    private GameManager.GameState relayState;
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            gm = GameObject.Find("Game Manager").GetComponent<GameManager>();
        }
        catch(System.Exception ex)
        {
            Debug.LogError("There was a problem with the Game Manager: " + ex.Message);
        }
    }

    public void ChangeState(string state)
    {
        //strange way to do this...!
        relayState = (GameManager.GameState)Enum.Parse(typeof(GameManager.GameState), state);
        Debug.Log(relayState.ToString());
        gm.ChangeState(relayState);
    }

    //UI related relay method
    public void GetPauseMenu()
    {
        gm.GetPauseMenu();
    }

    public void GetOptionsMenu()
    {
        gm.GetOptionsMenu();
    }

}
