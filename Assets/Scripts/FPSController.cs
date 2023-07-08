using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [Header("Move Control")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private CharacterStatus _characterStatus;
    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _jumpHight = 2f;

    [Header("Ground Check")]
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private float _groundCheckDistance = 1f;
    [SerializeField] private bool _isGrounded = false;
    [SerializeField] private bool _isGroundedOnGravityZone = false;

    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _gravityZoneMask;


    private void Awake()
    {
    }

    private void OnDestroy()
    {
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

            case CharacterStatus.TopOfGravityZone:
                CheckMoveInput();
                CheckIsGroundedOnGravityZone();
                CheckJumpInput();
                break;

            case CharacterStatus.InGravityZone:
                CheckMoveInput();
                break;
        }
    }

    private void CheckJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) 
            && (_isGrounded || _isGroundedOnGravityZone))
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

    private void CheckIsGroundedOnGravityZone()
    {
        _isGroundedOnGravityZone = Physics.CheckSphere(
            _groundCheckPoint.position,
            _groundCheckDistance,
            _gravityZoneMask);

        if (_isGroundedOnGravityZone && _velocity.y < 0)
        {
            OnCharacterStatusChanged(CharacterStatus.TopOfGravityZone);
        }
        else
        {
            OnCharacterStatusChanged(CharacterStatus.Normal);
        }
    }

    private void CheckMoveInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.right * x + transform.forward * z;

        _characterController.Move(moveDir * _speed * Time.deltaTime);
    }

    #region Listeners
    public void OnCharacterStatusChanged(CharacterStatus status)
    {
        if (_characterStatus == status) return;
        _characterStatus = status;
    }
    #endregion
}

public enum CharacterStatus
{
    Normal,    
    TopOfGravityZone,
    InGravityZone,
}
