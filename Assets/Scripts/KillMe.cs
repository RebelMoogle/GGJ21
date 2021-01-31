using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMe : MonoBehaviour
{
    public bool isKill;
    [Range(0.1f,3f)]
    public float killMeWhen = 1f;

    void Start()
    {
        if (isKill)
        {
            StartCoroutine(KillTimer(killMeWhen));
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }

    public void KillLater(float timeToDie)
    {
        StartCoroutine(KillTimer(timeToDie));
    }

    IEnumerator KillTimer(float timeToDie)
    {
        yield return new WaitForSeconds(timeToDie);

        Destroy(gameObject);
    }
}
