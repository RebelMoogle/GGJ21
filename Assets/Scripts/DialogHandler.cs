using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogHandler : MonoBehaviour
{
    public GameObject quipLeftPrfab;
    public GameObject quipRightPrefab;

    public Canvas canvas;
    
    public CharacterQuips playerOne;
    public CharacterQuips playerTwo;

    public UnityEvent afterStartQuipEvent;


    void Start()
    {
    }

    public void DialogStart()
    {
        StartCoroutine(StartingQuips());
    }

    IEnumerator StartingQuips()
    {
        Quip(playerOne, quipLeftPrfab, "start");

        yield return new WaitForSeconds(2.1f);

        Quip(playerTwo, quipRightPrefab, "start");

        afterStartQuipEvent.Invoke();
    }

    public void DialogWinRound()
    {
        StartCoroutine(PlayerWonRoundQuips());
        AudioController.Instance.StopMusic(true);
    }

    IEnumerator PlayerWonRoundQuips()
    {
        Quip(playerOne, quipLeftPrfab, "seducing");

        yield return new WaitForSeconds(2.1f);

        Quip(playerTwo, quipRightPrefab, "naked");
    }

    public void DialogLostRound()
    {
        StartCoroutine(PlayerLostRoundQuips());
        AudioController.Instance.StopMusic(true);
    }

    IEnumerator PlayerLostRoundQuips()
    {
        Quip(playerOne, quipLeftPrfab, "naked");

        yield return new WaitForSeconds(2.1f);

        Quip(playerTwo, quipRightPrefab, "seducing");
    }

    public void DialogWonMatch()
    {
        StartCoroutine(PlayerWonMatchQuips());
        AudioController.Instance.StopMusic(true);
    }

    IEnumerator PlayerWonMatchQuips()
    {
        Quip(playerOne, quipLeftPrfab, "victory");

        yield return new WaitForSeconds(2.1f);

        Quip(playerTwo, quipRightPrefab, "defeat");
    }

    public void DialogLostMatch()
    {
        StartCoroutine(PlayerLostMatchQuips());
        AudioController.Instance.StopMusic(true);
    }

    IEnumerator PlayerLostMatchQuips()
    {
        Quip(playerOne, quipLeftPrfab, "defeat");

        yield return new WaitForSeconds(2.1f);

        Quip(playerTwo, quipRightPrefab, "victory");
    }

    public void Quip(CharacterQuips character, GameObject quipType, string quipContent)
    {

        GameObject characterQuip = Instantiate(quipType, canvas.transform);

        //I couldn't get the sprite to swap, so I just defined it in the Prefab

        //SpriteRenderer characterSprite = characterQuip.transform.GetChild(0).GetComponent<SpriteRenderer>();
        //characterSprite.sprite = character.characterSplash;

        Text characterName = characterQuip.transform.GetChild(2).GetChild(0).GetComponent<Text>();
        characterName.text = character.characterName;

        switch (quipContent)
        {
            case "start":
                Text quipStart = characterQuip.transform.GetChild(1).GetChild(0).GetComponent<Text>();
                quipStart.text = character.introductions[Random.Range(0, character.introductions.Length)];
                break;
            case "naked":
                Text quipNaked = characterQuip.transform.GetChild(1).GetChild(0).GetComponent<Text>();
                quipNaked.text = character.nakedResponses[Random.Range(0, character.introductions.Length)];
                break;
            case "defeat":
                Text quipDefeat = characterQuip.transform.GetChild(1).GetChild(0).GetComponent<Text>();
                quipDefeat.text = character.defeatResponses[Random.Range(0, character.introductions.Length)];
                break;
            case "seducing":
                Text quipSeducing = characterQuip.transform.GetChild(1).GetChild(0).GetComponent<Text>();
                quipSeducing.text = character.seducingResponses[Random.Range(0, character.introductions.Length)];
                break;
            case "victory":
                Text quipVictory = characterQuip.transform.GetChild(1).GetChild(0).GetComponent<Text>();
                quipVictory.text = character.victoryResponses[Random.Range(0, character.introductions.Length)];
                break;
            default:
                Text quip = characterQuip.transform.GetChild(1).GetChild(0).GetComponent<Text>();
                quip.text = character.introductions[Random.Range(0, character.introductions.Length)];
                break;
        }

        
        KillMe killswitch = characterQuip.GetComponent<KillMe>();
        killswitch.KillLater(2f);
    }




}
