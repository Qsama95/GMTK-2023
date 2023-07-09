using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBox : MonoBehaviour
{
    public GameObject respawnPosition;
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "DeathZone")
        {
            gameObject.transform.position = respawnPosition.transform.position;
        }
    }
}
