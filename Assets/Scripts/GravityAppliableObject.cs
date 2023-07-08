using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAppliableObject : MonoBehaviour, IGravityAppliable
{
    [SerializeField , Range(25, 100)] 
    private float _externalForceFactor = 50;
    private Rigidbody _rigidbody; 

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnApplyExternalGravity(
        Vector3 dir, 
        float force, 
        bool _inverseForce)
    {
        _rigidbody.useGravity = false;

        var inverseFactor = _inverseForce ? -1 : 1;
        _rigidbody.velocity = dir *
            force *
            inverseFactor *
            _externalForceFactor *
            Time.deltaTime;
    }

    public void OnReleaseFromExternalGravity()
    {
        _rigidbody.useGravity = true;
    }

    public void OnGravityChanged()
    {
        throw new System.NotImplementedException();
    }
}
