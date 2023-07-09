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
    [SerializeField] private bool _isGroundedOnMovingPlatform = false;

    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private LayerMask _gravityZoneMask;
    [SerializeField] private LayerMask _movingPlatformMask;

    public CharacterStatus CharacterStatus { 
        get => _characterStatus; set => _characterStatus = value; }

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
                CheckIsGroundedOnMovingPlatform();
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

            case CharacterStatus.OnMovingPlatform:
                CheckMoveInput();
                CheckIsGroundedOnMovingPlatform();
                CheckJumpInput();
                break;
        }
    }

    private void CheckJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) 
            && (_isGrounded || 
            _isGroundedOnGravityZone || 
            _isGroundedOnMovingPlatform))
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

    private void CheckIsGroundedOnMovingPlatform()
    {
        _isGroundedOnMovingPlatform = Physics.CheckSphere(
            _groundCheckPoint.position,
            _groundCheckDistance,
            _movingPlatformMask);

        if (_isGroundedOnMovingPlatform && _velocity.y < 0)
        {
            _velocity.y = -2f;
            var colliders = Physics.OverlapSphere(
            _groundCheckPoint.position,
            _groundCheckDistance,
            _movingPlatformMask);

            if (_isMovingByPlayer) return;
            if (colliders.Length == 0) return;
            var tempVelocity = colliders[0].GetComponent<Rigidbody>().velocity;
            _characterController.Move(tempVelocity * Time.deltaTime);

            OnCharacterStatusChanged(CharacterStatus.OnMovingPlatform);
        }
        else
        {
            OnCharacterStatusChanged(CharacterStatus.Normal);
        }
    }

    private bool _isMovingByPlayer;

    private void CheckMoveInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.right * x + transform.forward * z;

        if (moveDir.magnitude >= 0.01f)
        {
            _isMovingByPlayer = true;
        }
        else
        {
            _isMovingByPlayer = false;
        }
        _characterController.Move(moveDir * _speed * Time.deltaTime);
    }

    #region Listeners
    public void OnCharacterStatusChanged(CharacterStatus status)
    {
        if (_characterStatus == status) return;

        if (_characterStatus == CharacterStatus.OnMovingPlatform &&
            status == CharacterStatus.InGravityZone) return;

        _characterStatus = status;
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IGravityAppliable>() != null)
        {
            other.GetComponent<IGravityAppliable>().IsPlayerOnIt = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<IGravityAppliable>() != null)
        {
            other.GetComponent<IGravityAppliable>().IsPlayerOnIt = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IGravityAppliable>() != null)
        {
            other.GetComponent<IGravityAppliable>().IsPlayerOnIt = false;
        }
    }
}

public enum CharacterStatus
{
    Normal,    
    TopOfGravityZone,
    InGravityZone,
    OnMovingPlatform
}
