using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneHandler : MonoBehaviour
{
    public GameObject transition_prefab;
    public Canvas canvas;

    public void LoadScene (int scene)
    {
        StartCoroutine(LoadTimer(scene));
        Instantiate(transition_prefab, canvas.transform);
    }

    IEnumerator LoadTimer (int scene)
    {
        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(scene);
    }
}
