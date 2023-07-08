using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextLevel : MonoBehaviour
{
    public string sceneToLoad;
    // Start is called before the first frame update
    public void OnInteract()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}