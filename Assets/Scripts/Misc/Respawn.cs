using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    public Animator screenDimmer;
    public GameObject roomSpawnPoint;
    private void OnTriggerEnter (Collider other) {
        if (other.tag == "DeathZone") {
            screenDimmer.SetBool ("HasDied", true);
            StartCoroutine(RespawnTimer());
        }
    }

    private IEnumerator RespawnTimer () {
        yield return new WaitForSeconds (1);
        gameObject.transform.position = roomSpawnPoint.transform.position;
        screenDimmer.SetBool ("HasDied", false);
    }

}