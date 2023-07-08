using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityApplier : MonoBehaviour
{
    [SerializeField] 
    private bool _inverseForce;
    [SerializeField, Range(0, 10)]
    private float _force = 5;

    private Vector3 _centerPos;
    private GravityEmitter _gravityEmitter;
    private CharacterController _characterController;

    private void Awake()
    {
        _gravityEmitter = GetComponentInParent<GravityEmitter>();
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _centerPos = transform.position;
        _gravityEmitter.ApplyFowardForce.AddListener(SetFowardForce);
        _gravityEmitter.ApplyBackwardForce.AddListener(SetBackwardForce);
    }

    private void OnDestroy()
    {
        _gravityEmitter.ApplyFowardForce.RemoveListener(SetFowardForce);
        _gravityEmitter.ApplyBackwardForce.RemoveListener(SetBackwardForce);
    }

    private void Update()
    {
        ApplyForceOnPlayer();
    }

    private void SetFowardForce()
    {
        _inverseForce = false;
    }

    private void SetBackwardForce()
    {
        _inverseForce = true;
    }

    private void ApplyForceOnPlayer()
    {
        if (_characterController)
        {
            var inverseFactor = _inverseForce ? -1 : 1;
            _characterController.Move(
                transform.up * _force * inverseFactor * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if it is player
        if (other.tag.CompareTo("Player") == 0)
        {
            // change player status
            var fpsController = other.GetComponent<FPSController>();
            fpsController.OnCharacterStatusChanged(CharacterStatus.InGravityZone);
            var controller = other.GetComponent<CharacterController>();
            PlayerEntered(controller);
        }
        // check if it is object can be applied on gravity
        if (other.tag.CompareTo("GravityAppliableObject") == 0)
        {
            var appliableObj = other.GetComponent<GravityAppliableObject>();
            appliableObj.OnApplyExternalGravity(transform.up, _force, _inverseForce);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // check if it is player
        if (other.tag.CompareTo("Player") == 0)
        {
            if (_gravityEmitter.IsPlayerControllerOccupied()) return;
            // change player status
            var fpsController = other.GetComponent<FPSController>();
            fpsController.OnCharacterStatusChanged(CharacterStatus.InGravityZone);
            var controller = other.GetComponent<CharacterController>();
            PlayerEntered(controller);
        }
        // check if it is object can be applied on gravity
        if (other.tag.CompareTo("GravityAppliableObject") == 0)
        {
            var appliableObj = other.GetComponent<GravityAppliableObject>();
            appliableObj.OnApplyExternalGravity(transform.up, _force, _inverseForce);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // check if it is player
        if (other.tag.CompareTo("Player") == 0)
        {
            // change player status
            var fpsController = other.GetComponent<FPSController>();
            fpsController.OnCharacterStatusChanged(CharacterStatus.TopOfGravityZone);
            PlayerEntered(null);
        }
        // check if it is object can be applied on gravity
        if (other.tag.CompareTo("GravityAppliableObject") == 0)
        {
            var appliableObj = other.GetComponent<GravityAppliableObject>();
            appliableObj.OnReleaseFromExternalGravity();
        }
    }

    private void PlayerEntered(CharacterController controller)
    {
        if (_gravityEmitter.IsPlayerControllerOccupied()) return;

        // first time player enter confirm
        _characterController = controller;
    }

    private IEnumerator MoveObjectToCenterRay(Transform objTransform)
    {
        var distance = 100f;
        var targetPos = _centerPos + transform.up;
        targetPos.y = objTransform.position.y;

        while (distance > 0.01f)
        {
            UpdateDistance(objTransform);

            yield return null;
        }
    }

    private void UpdateDistance(Transform objTransform)
    {
    }
}
