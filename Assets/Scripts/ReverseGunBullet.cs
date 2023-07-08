using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReverseGunBullet : MonoBehaviour
{
    public LayerMask UnHitableLayerMask;
    public UnityEvent HitOnNotInverable;
    public UnityEvent HitOnInversable;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IInversable>() != null)
        {
            other.GetComponent<IInversable>().OnHitByReverseGunBullet();
            HitOnInversable?.Invoke();
            Destroy(gameObject);
        }
        else if (other.gameObject.layer != UnHitableLayerMask)
        {
            HitOnNotInverable?.Invoke();
            Destroy(gameObject);
        }
    }
}
