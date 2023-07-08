using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Controllers/ReverseGunController",
    fileName = "ReverseGunController")]
public class ReverseGunController : ScriptableObject
{

    [SerializeField] private Transform _attachedObject;

    [SerializeField] private Transform _gun;

    public void SetReverseGun(Transform gun)
    {
        _gun = gun;
    }

    public void SetAttachedObject(Transform newParent, Transform obj)
    {
        if (_attachedObject) return;

        _attachedObject = obj;
        _attachedObject.SetParent(newParent);
    }
}
