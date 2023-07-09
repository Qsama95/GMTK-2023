using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
    public string sceneToLoad;
    public Animator screenDimmer;

    // Update is called once per frame
    public void NewGame () {
        screenDimmer.SetTrigger ("StartGame");
        StartCoroutine (LoadNewGame ());
    }

    IEnumerator LoadNewGame () {
        yield return new WaitForSeconds (1);
        SceneManager.LoadScene (sceneToLoad);
    }
}