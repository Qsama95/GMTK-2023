using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour {
    public GameObject raycastOrigin;
    public float maxRaycastDistance = 2;
    public GameObject interactableQueue;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update () {
        CheckInteraction ();
    }

    private void CheckInteraction () {
        RaycastHit raycastHit;
        if (Physics.Raycast (raycastOrigin.transform.position, raycastOrigin.transform.forward, out raycastHit, maxRaycastDistance)) {
            if (raycastHit.transform.gameObject.tag == "isInteractable") {
                interactableQueue.gameObject.SetActive (true);
                if (Input.GetKeyDown (KeyCode.E)) {
                    raycastHit.transform.gameObject.GetComponent<LoadNextLevel> ().OnInteract ();
                }
            }
        } else {
            interactableQueue.gameObject.SetActive (false);
        }
    }
}