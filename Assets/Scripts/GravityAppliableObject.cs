using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAppliableObject : MonoBehaviour, IGravityAppliable
{
    [SerializeField , Range(25, 100)] 
    private float _externalForceFactor = 50;
    [SerializeField]
    private GravityAppliableObjectType _objectType;

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
        switch (_objectType)
        {
            case GravityAppliableObjectType.MovingPlatform:
                _rigidbody.useGravity = false;
                _rigidbody.velocity = Vector3.zero;

                break;
            case GravityAppliableObjectType.FreeObject:
                _rigidbody.useGravity = true;
                break;
        }
    }

    public void OnGravityChanged()
    {

    }
}

public enum GravityAppliableObjectType
{
    MovingPlatform,
    FreeObject
}
