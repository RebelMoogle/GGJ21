using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fx_Spawner : MonoBehaviour
{
    public float timeBetweenSpawns = 3;
    public GameObject[] fxTest;
    private bool waiting;

    // Update is called once per frame
    void Update()
    {
        if (!waiting)
        {
            StartCoroutine(fxSpawnTimer());
        }
    }

    IEnumerator fxSpawnTimer()
    {
        waiting = true;
        Instantiate(fxTest[Random.Range(0, fxTest.Length)]);

        yield return new WaitForSeconds(timeBetweenSpawns);

        waiting = false;
    }
}
