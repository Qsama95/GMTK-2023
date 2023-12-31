using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReverseGunBullet : MonoBehaviour {
    public UnityEvent HitOnNotInverable;
    public UnityEvent HitOnInversable;
    public GameObject HitMissSound;
    public GameObject HitSound;
    public GameObject bulletHitEffect;

    private void Start () {
        Destroy (gameObject, 5f);
    }

    private void OnTriggerEnter (Collider other) {
        if (other.GetComponent<IHitAttachable> () != null) {
            other.GetComponent<IHitAttachable> ().OnHitByReverseGunBullet ();
            HitOnInversable?.Invoke ();
            Destroy (gameObject);
            GameObject hitSound = Instantiate (HitSound, transform.position, Quaternion.identity);
            Destroy (hitSound, 2);
            GameObject bulletHit = Instantiate (bulletHitEffect, transform.position, Quaternion.identity);
            Destroy (bulletHit, 1);
        } else if (other.gameObject.layer != 7) {
            HitOnNotInverable?.Invoke ();
            GameObject hitSound = Instantiate (HitMissSound, transform.position, Quaternion.identity);
            Destroy (hitSound, 2);
            Destroy (gameObject);
            GameObject bulletHit = Instantiate (bulletHitEffect, transform.position, Quaternion.identity);
            Destroy (bulletHit, 1);
        }

    }
}