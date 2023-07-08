using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Move Control")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private GravityController _gravityController;
    [SerializeField] private CharacterStatus _characterStatus;
    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _jumpHight = 2f;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private float _groundCheckDistance = 1f;
    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private LayerMask _groundMask;

    private void Awake()
    {
        _gravityController.ChangeGravity.AddListener(OnCharacterStatusChanged);
    }

    private void OnDestroy()
    {
        _gravityController.ChangeGravity.RemoveListener(OnCharacterStatusChanged);
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        UpdateCharacterStatus();
    }

    private void UpdateCharacterStatus()
    {
        switch (_characterStatus)
        {
            case CharacterStatus.Normal:
                CheckMoveInput();
                CheckIsGrounded();
                CheckJumpInput();
                break;

            case CharacterStatus.InGravityZone:
                CheckMoveInput();
                break;
        }
    }

    private void CheckJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHight * -2f * _gravity);
        }
    }

    private void CheckIsGrounded()
    {
        _isGrounded = Physics.CheckSphere(
            _groundCheckPoint.position,
            _groundCheckDistance,
            _groundMask);

        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);
    }

    private void CheckMoveInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.right * x + transform.forward * z;

        _characterController.Move(moveDir * _speed * Time.deltaTime);
    }

    #region Listeners
    private void OnCharacterStatusChanged(CharacterStatus status)
    {
        _characterStatus = status;
    }
    #endregion
}

public enum CharacterStatus
{
    Normal,    
    InGravityZone,
}
