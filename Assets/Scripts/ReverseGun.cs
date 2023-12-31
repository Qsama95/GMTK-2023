using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReverseGun : MonoBehaviour, IFunctionInversable
{
    [SerializeField] private ReverseGunBulletContainer _bulletContainer;
    [SerializeField] private ReverseGunController _gunController;
    [SerializeField] private Transform _muzzleTransform;

    [SerializeField, Range(1, 20)] private float _shootForce = 10;

    public UnityEvent OnShoot;
    public UnityEvent ToggleGravity;

    public UnityEvent GravityPullOn;
    public UnityEvent GravityPushOn;

    public Transform MuzzleTransform { get => _muzzleTransform; set => _muzzleTransform = value; }
    public bool IsReversed { get; set; }

    private void Awake()
    {
        _gunController.SetReverseGun(this);
    }

    void Start()
    {
        Init();
    }

    private void Init()
    {
        ToggleGravity.AddListener(_gunController.ReverseObjFunction);
        ToggleGravity.AddListener(_gunController.ReverseGunView);
    }

    private void OnDestroy()
    {
        ToggleGravity.RemoveListener(_gunController.ReverseObjFunction);
        ToggleGravity.RemoveListener(_gunController.ReverseGunView);
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

        if (Input.GetMouseButtonDown(1))
        {
            // reverse attached obj function
            ToggleGravity?.Invoke();
        }
    }

    private void Shoot()
    {
        var newBullet = _bulletContainer.SpawnBullet(_muzzleTransform.position);
        var newBulletRb = newBullet.GetComponent<Rigidbody>();
        newBulletRb.AddForce(_muzzleTransform.forward * _shootForce, ForceMode.Impulse);
        OnShoot?.Invoke();
    }

    public void OnToggleFunctionInverse()
    {
        if (IsReversed)
        {
            GravityPushOn?.Invoke();
        }
        else
        {
            GravityPullOn?.Invoke();
        }
    }
}
