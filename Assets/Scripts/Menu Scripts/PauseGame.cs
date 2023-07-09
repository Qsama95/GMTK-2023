using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
    public GameObject pauseMenu;
    public ReverseGun reverseGun;
    private void Update () {
        if (Input.GetKeyDown (KeyCode.Escape)) {
            if (!pauseMenu.active) {
                Pause ();
            }
            else
            { 
                UnPause();
            }
        }
    }

    public void Pause () {
        reverseGun.enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        pauseMenu.SetActive (true);
        Time.timeScale = 0;
    }

    public void UnPause () {
        reverseGun.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}