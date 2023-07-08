using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour {

    public Animator screenDimmer;
    private void OnTriggerEnter (Collider other) {
        if (other.tag == "DeathZone") {
            screenDimmer.SetBool ("HasDied", true);
            StartCoroutine(RespawnTimer());
        }
    }

    private IEnumerator RespawnTimer () {
        yield return new WaitForSeconds (1);
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

}