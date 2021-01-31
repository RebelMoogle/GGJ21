using System;
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
        Time.timeScale = 0;
        //Time.fixedDeltaTime = 0;
        gm.GetPauseMenu();
    }

    public void ResumeGame()
    {
        //Time.fixedDeltaTime = 1;
        Time.timeScale = 1;
    }

    public void GetOptionsMenu()
    {
        gm.GetOptionsMenu();
    }

}
