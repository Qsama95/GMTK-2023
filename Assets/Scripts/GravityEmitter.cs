using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityEmitter : MonoBehaviour, IInversable
{
    [SerializeField] private GravityController _gravityController;
    [SerializeField] private Transform _gravityApplier;
    [SerializeField] private bool _isReversed = false;

    public UnityEvent ApplyFowardForce;
    public UnityEvent ApplyBackwardForce;

    public bool HasGravityApplier => _gravityApplier;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (_isReversed)
        {
            ApplyBackwardForce?.Invoke();
        }
        else
        {
            ApplyFowardForce?.Invoke();
        }
    }

    public void ChangeGravity(CharacterStatus status)
    {
        _gravityController.ChangeGravity?.Invoke(status);
    }

    public bool IsPlayerControllerOccupied()
    {
        return _gravityController.PlayerControlOccupied;
    }

    public void OnHitByReverseGunBullet()
    {
        if (_isReversed)
        {
            ApplyFowardForce?.Invoke();
        }
        else
        {
            ApplyBackwardForce?.Invoke();
        }
        _isReversed = !_isReversed;
    }

    public void SetParent(Transform newParent)
    {
        if (_gravityApplier)
        {
            _gravityApplier.transform.SetParent(newParent);
        }
    }
}
