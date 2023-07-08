using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Respawn : MonoBehaviour {

    public Animator screenDimmer;
    public AudioSource deathSound;
    private void OnTriggerEnter (Collider other) {
        if (other.tag == "DeathZone") {
            screenDimmer.SetBool ("HasDied", true);
            StartCoroutine(RespawnTimer());
            deathSound.Play();
        }
    }

    private IEnumerator RespawnTimer () {
        yield return new WaitForSeconds (1);
        Scene scene = SceneManager.GetActiveScene();
        deathSound.Stop();
        SceneManager.LoadScene(scene.name);
    }

}