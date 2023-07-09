using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {
    public GameObject playerCam;
    public float maxRaycastDistance = 2;
    public GameObject interactableQueue;
    public GameObject reverseGun;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update () {
        CheckInteraction ();
    }

    private void CheckInteraction () {
        RaycastHit raycastHit;
        if (Physics.Raycast (playerCam.transform.position, playerCam.transform.forward, out raycastHit, maxRaycastDistance)) {
            if (raycastHit.transform.gameObject.tag == "isInteractable") {
                interactableQueue.gameObject.SetActive (true);
                if (Input.GetKeyDown (KeyCode.E)) {
                    if (raycastHit.transform.gameObject.GetComponent<LoadNextLevel> ()) {
                        raycastHit.transform.gameObject.GetComponent<LoadNextLevel> ().OnInteract ();
                    }
                    if (raycastHit.transform.gameObject.name == "GunPickUp") {
                        reverseGun.SetActive (true);
                        raycastHit.transform.gameObject.SetActive(false);
                    }
                }
            }
        } else {
            interactableQueue.gameObject.SetActive (false);
        }
    }
}