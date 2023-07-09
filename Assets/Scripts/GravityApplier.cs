using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityApplier : MonoBehaviour , IFunctionInversable
{

    [SerializeField]
    private GravityController _gravityController;
    [SerializeField]
    private bool _inverseForce;
    [SerializeField, Range (0, 10)]
    private float _force = 5;

    private Vector3 _centerPos;
    private CharacterController _characterController;

    public bool IsUsingByPlayer;
    public AudioSource gravityFieldApplySound;

    public UnityEvent GravityPullOn;
    public UnityEvent GravityPushOn;

    private List<GameObject> _objectsInTheZone = new List<GameObject>();

    public bool IsReversed { get => _inverseForce; set => _inverseForce = value; }

    private void Awake () {
    }

    private void Start () {
        Init ();
    }

    private void Init () {
        _centerPos = transform.position;
        ChangeGravityEvent();
    }

    private void OnDestroy () {
    }

    private void Update () {
        ApplyForceOnPlayer ();
        CheckIfUsingByPlayer();
    }

    private void CheckIfUsingByPlayer()
    {
        IsUsingByPlayer = transform.root.GetComponent<CharacterController>();
    }

    private void ApplyForceOnPlayer () {
        if (_characterController) {
            if (!gravityFieldApplySound.isPlaying) {
                gravityFieldApplySound.Play ();
            }

            if (_characterController.GetComponent<FPSController>().CharacterStatus
                == CharacterStatus.OnMovingPlatform) return;
            var inverseFactor = _inverseForce ? -1 : 1;
            _characterController.Move (
                transform.up * -_force * inverseFactor * Time.deltaTime);
        }
    }

    private void OnTriggerEnter (Collider other) {
        // check if it is player
        if (other.tag.CompareTo ("Player") == 0) {
            // change player status
            var fpsController = other.GetComponent<FPSController> ();
            fpsController.OnCharacterStatusChanged (CharacterStatus.InGravityZone);
            var controller = other.GetComponent<CharacterController> ();
            StartCoroutine(MoveObjectToCenterRay(controller));
            PlayerEntered(controller);

            if (_objectsInTheZone.Contains(fpsController.gameObject)) return;
            _objectsInTheZone.Add(fpsController.gameObject);
        }
        // check if it is object can be applied on gravity
        if (other.tag.CompareTo ("GravityAppliableObject") == 0) {
            var appliableObj = other.GetComponent<GravityAppliableObject> ();
            appliableObj.OnApplyExternalGravity (
                transform.up, _force, _inverseForce, IsUsingByPlayer);
            if (_objectsInTheZone.Contains(appliableObj.gameObject)) return;
            _objectsInTheZone.Add(appliableObj.gameObject);
        }
    }

    //private void OnTriggerStay (Collider other) {
    //    // check if it is player
    //    if (other.tag.CompareTo ("Player") == 0) {
    //        if (_gravityController.PlayerControlOccupied) return;
    //        // change player status
    //        var fpsController = other.GetComponent<FPSController> ();
    //        fpsController.OnCharacterStatusChanged (CharacterStatus.InGravityZone);
    //        var controller = other.GetComponent<CharacterController> ();
    //        PlayerEntered (controller);
    //    }
    //    // check if it is object can be applied on gravity
    //    if (other.tag.CompareTo ("GravityAppliableObject") == 0) {
    //        var appliableObj = other.GetComponent<GravityAppliableObject> ();
    //        appliableObj.OnApplyExternalGravity (
    //            transform.up, _force, _inverseForce, IsUsingByPlayer);
    //    }
    //}

    private void OnTriggerExit (Collider other) {
        // check if it is player
        if (other.tag.CompareTo ("Player") == 0) {
            // change player status
            var fpsController = other.GetComponent<FPSController> ();
            fpsController.OnCharacterStatusChanged (CharacterStatus.TopOfGravityZone);
            PlayerEntered (null);
            gravityFieldApplySound.Stop();
            if (!_objectsInTheZone.Contains(fpsController.gameObject)) return;
            _objectsInTheZone.Remove(fpsController.gameObject);
        }
        // check if it is object can be applied on gravity
        if (other.tag.CompareTo ("GravityAppliableObject") == 0) {
            var appliableObj = other.GetComponent<GravityAppliableObject> ();
            appliableObj.OnReleaseFromExternalGravity ();
            if (!_objectsInTheZone.Contains(appliableObj.gameObject)) return;
            _objectsInTheZone.Remove(appliableObj.gameObject);
        }
    }

    private void PlayerEntered (CharacterController controller) {
        if (_gravityController.PlayerControlOccupied) return;

        // first time player enter confirm
        _characterController = controller;
    }

    private IEnumerator MoveObjectToCenterRay (CharacterController controller) {
        var timeCount = 1f;
        
        while (timeCount > 0f) {
            timeCount -= Time.deltaTime;
            var dir = controller.transform.position - _centerPos;
            var normal = Vector3.Cross(transform.up, dir);
            var targetDir = Vector3.Cross(normal, transform.up);
            controller.Move(-targetDir.normalized * _force/5 * Time.deltaTime);
            yield return null;
        }
    }

    public void ReleaseAll()
    {
        // release all objects in zone
        if (_objectsInTheZone.Count > 0)
        {
            foreach (GameObject obj in _objectsInTheZone)
            {
                if (obj.GetComponent<IGravityAppliableBase>() != null) 
                {
                    obj.GetComponent<IGravityAppliableBase>().OnGravityReleased();
                }
            }
        }
    }

    public void OnToggleFunctionInverse()
    {
        _inverseForce = !_inverseForce;
        ChangeGravityEvent();
    }

    private void ChangeGravityEvent()
    {
        if (_inverseForce)
        {
            GravityPushOn?.Invoke();
        }
        else
        {
            GravityPullOn?.Invoke();
        }
    }
}