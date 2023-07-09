using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonLoadLevel : MonoBehaviour {
    public string sceneToLoad;
    public Animator screenDimmer;

    // Update is called once per frame
    public void LoadLevel () {
        screenDimmer.SetTrigger ("StartGame");
        StartCoroutine (LoadSelectedLevel ());
    }

    IEnumerator LoadSelectedLevel () {
        yield return new WaitForSeconds (1);
        SceneManager.LoadScene (sceneToLoad);
    }
}