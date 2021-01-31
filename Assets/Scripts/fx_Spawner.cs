using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fx_Spawner : MonoBehaviour
{
    public float timeBetweenSpawns = 3; //only used for the random spawn timer
    public GameObject[] fxTest;
    public GameObject hitPrefab;
    public GameObject fallPrefab;
    public GameObject jumpPrefab;
    public GameObject landPrefab;
    public GameObject kissPrefab;
    public GameObject muskPrefab;
    public GameObject popperPrefab;
    private bool waiting;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnFXJump()
    {
        GameObject jump = Instantiate(jumpPrefab);
        //TODO: set position of the object
    }

    public void SpawnFXLand()
    {
        GameObject jump = Instantiate(jumpPrefab);
        //TODO: set position of the object
    }

    public void SpawnFXFall()
    {
        GameObject jump = Instantiate(jumpPrefab);
        //TODO: set position of the object
    }

    public void SpawnFXHit()
    {
        GameObject jump = Instantiate(jumpPrefab);
        //TODO: set position of the object
    }

    public void RandomSpawn()
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
