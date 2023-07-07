using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSController : MonoBehaviour
{
    [SerializeField] private Camera _characterCamera;

    [SerializeField] private CharacterController _characterController;

    [SerializeField] private float _speed = 12f;

    void Start()
    {
        
    }

    void Update()
    {
        CheckMoveInput();
    }

    private void CheckMoveInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDir = transform.right * x + transform.forward * z;

        _characterController.Move(moveDir * _speed * Time.deltaTime);
    }
}
