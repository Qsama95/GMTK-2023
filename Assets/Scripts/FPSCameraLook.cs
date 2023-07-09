using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSCameraLook : MonoBehaviour
{
    [SerializeField] private float _mouseSensitivity = 100f;

    [SerializeField] private Transform _playerTransform;

    private float xRotation = 0f;

    void Start()
    {
        transform.localRotation = Quaternion.Euler(0, 0f, 0f);
    }

    void Update()
    {
        CheckMousePosition();
    }

    private void CheckMousePosition()
    {
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -85, 85f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        _playerTransform.Rotate(Vector3.up * mouseX);
    }
}
