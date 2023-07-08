using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReverseGun : MonoBehaviour
{
    [SerializeField] private ReverseGunBulletContainer _bulletContainer;
    [SerializeField] private Transform _muzzleTransform;

    [SerializeField, Range(1, 20)] private float _shootForce = 10;

    public UnityEvent OnShoot;

    void Start()
    {
        
    }

    void Update()
    {
        CheckPlayerInput();
    }

    private void CheckPlayerInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // shoot bullet
            Shoot();
        }
    }

    private void Shoot()
    {
        var newBullet = _bulletContainer.SpawnBullet(_muzzleTransform.position);
        var newBulletRb = newBullet.GetComponent<Rigidbody>();
        newBulletRb.AddForce(_muzzleTransform.forward * _shootForce, ForceMode.Impulse);
        OnShoot?.Invoke();
    }
}
