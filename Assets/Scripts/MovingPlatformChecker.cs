using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformChecker : MonoBehaviour
{
    [SerializeField] private bool _isOnMovingPlatform;
    [SerializeField] private float _groundCheckDistance = 4f;
    [SerializeField] private LayerMask _movingPlatformMask;

    private void Update()
    {
        CheckMovingPlatform();
    }

    private RaycastHit _hit;
    private RaycastHit _lastHit;

    private void CheckMovingPlatform()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(ray, out _hit, _groundCheckDistance, _movingPlatformMask))
        {
            _lastHit = _hit;
            if (_hit.transform.GetComponent<IGravityAppliable>() != null)
            {
                _hit.transform.GetComponent<IGravityAppliable>().IsPlayerOnIt = true;
            }
        }
        else
        {
            if (_hit.transform == null && _lastHit.transform)
            {
                if (_lastHit.transform.GetComponent<IGravityAppliable>() != null)
                {
                    _lastHit.transform.GetComponent<IGravityAppliable>().IsPlayerOnIt = false;
                }
            }
        }
    }
}
