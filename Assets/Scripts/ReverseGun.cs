using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ReverseGun : MonoBehaviour, IFunctionInversable {
    [SerializeField] private ReverseGunBulletContainer _bulletContainer;
    [SerializeField] private ReverseGunController _gunController;
    [SerializeField] private Transform _muzzleTransform;

    [SerializeField, Range (1, 20)] private float _shootForce = 10;

    public UnityEvent OnShoot;
    public UnityEvent ToggleGravity;

    public UnityEvent GravityPullOn;
    public UnityEvent GravityPushOn;

    public UnityEvent<bool> RightMouseClick;

    public Transform MuzzleTransform { get => _muzzleTransform; set => _muzzleTransform = value; }
    public bool IsReversed { get; set; }

    private Animator _animator;
    private string _rightMousePressed = "RightMousePressed";
    private string _functionAttached = "FunctionAttached";
    private string _reversed = "Reversed";

    private void Awake () {
        _gunController.SetReverseGun (this);
        _animator = GetComponent<Animator> ();
    }

    void Start () {
        //Init ();
    }

    private void OnEnable()
    {
        Init();
    }

    private void OnDisable()
    {
        ToggleGravity.RemoveListener(_gunController.ReverseObjFunction);
        ToggleGravity.RemoveListener(_gunController.ReverseGunView);
        RightMouseClick.RemoveListener(_gunController.SetAttachObjectActive);

        _gunController.OnAttachObject -= OnAttachObject;
    }

    private void Init () {
        ToggleGravity.AddListener (_gunController.ReverseObjFunction);
        ToggleGravity.AddListener (_gunController.ReverseGunView);
        RightMouseClick.AddListener(_gunController.SetAttachObjectActive);

        _gunController.OnAttachObject += OnAttachObject;
    }

    //private void OnDestroy () {
    //    ToggleGravity.RemoveListener (_gunController.ReverseObjFunction);
    //    ToggleGravity.RemoveListener (_gunController.ReverseGunView);
    //    RightMouseClick.RemoveListener(_gunController.SetAttachObjectActive);

    //    _gunController.OnAttachObject -= OnAttachObject;
    //}

    void Update () {
        CheckPlayerInput ();
    }

    private void CheckPlayerInput () {
        if (Input.GetMouseButtonDown (0)) {
            // shoot bullet
            Shoot ();
        }

        if (Input.GetMouseButton (1)) {
            RightMouseClick?.Invoke (true);
            _animator.SetBool(_rightMousePressed, true);
        }
        else {
            RightMouseClick?.Invoke (false);
            _animator.SetBool(_rightMousePressed, false);
        }

        if (Input.GetKeyDown (KeyCode.R)) {
            // reverse attached obj function
            ToggleGravity?.Invoke ();
        }
    }

    private void Shoot () {
        var newBullet = _bulletContainer.SpawnBullet (_muzzleTransform.position);
        var newBulletRb = newBullet.GetComponent<Rigidbody> ();
        newBulletRb.AddForce (_muzzleTransform.forward * _shootForce, ForceMode.Impulse);
        OnShoot?.Invoke ();
    }

    private void OnAttachObject (bool value) {
        _animator.SetBool (_functionAttached, value);
    }

    public void OnToggleFunctionInverse () {
        if (IsReversed) {
            GravityPushOn?.Invoke ();
        } else {
            GravityPullOn?.Invoke ();
        }
        _animator.SetBool(_reversed, IsReversed);
    }
}