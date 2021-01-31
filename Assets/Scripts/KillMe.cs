using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillMe : MonoBehaviour
{
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
