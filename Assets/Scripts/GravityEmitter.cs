using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityEmitter : MonoBehaviour
{
    [SerializeField] private GravityController _gravityController;

    [SerializeField] 
    private bool _inverseForce;
    [SerializeField, Range(0, 10)]
    private float _force = 5;
    private CharacterController _characterController;

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    private void Update()
    {
        ApplyForceOnPlayer();
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
            _gravityController.ChangeGravity?.Invoke(CharacterStatus.InGravityZone);
            var controller = other.GetComponent<CharacterController>();
            PlayerEntered(controller);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // check if it is player
        if (other.tag.CompareTo("Player") == 0)
        {
            if (_gravityController.PlayerControlOccupied) return;
            // change player status
            _gravityController.ChangeGravity?.Invoke(CharacterStatus.InGravityZone);
            var controller = other.GetComponent<CharacterController>();
            PlayerEntered(controller);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // check if it is player
        if (other.tag.CompareTo("Player") == 0)
        {
            // change player status
            _gravityController.ChangeGravity?.Invoke(CharacterStatus.Normal);
            PlayerEntered(null);
        }
    }

    private void PlayerEntered(CharacterController controller)
    {
        if (_gravityController.PlayerControlOccupied) return;
        _characterController = controller;
    }
}
