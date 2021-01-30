using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialog/Quip")]
public class CharacterQuips : ScriptableObject
{
    public string characterName;
    public Sprite characterSplash;

    public string[] introductions;
    public string[] nakedResponses;
    public string[] defeatResponses;
    public string[] seducingResponses;
    public string[] victoryResponses;
}
