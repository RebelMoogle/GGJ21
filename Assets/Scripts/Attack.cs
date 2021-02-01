
using UnityEngine;
using FMODUnity;

[System.Serializable]
public class Attack {
    public string attackName;
    public string animationName;
    public float range;
    public float areaOfEffect;
    public int damage;
    public Transform origin;
    public string soundClip; //This is meant for playing sounds during attacks, can be left empty.
}