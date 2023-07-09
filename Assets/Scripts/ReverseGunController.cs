using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Controllers/ReverseGunController",
    fileName = "ReverseGunController")]
public class ReverseGunController : ScriptableObject
{
    [SerializeField] private GameObject _attachedObject;

    [SerializeField] private ReverseGun _gun;

    public ReverseGun Gun { get => _gun; set => _gun = value; }
    public GameObject AttachedObject { get => _attachedObject; set => _attachedObject = value; }

    public void SetReverseGun(ReverseGun gun)
    {
        _gun = gun;
    }

    private void OnEnable()
    {
        Gun = null;
        AttachedObject = null;
    }

    public void AttachObjectOnGun(
        GameObject obj, 
        Action onAttachedOnGun)
    {
        if (_attachedObject) return;
        var oldName = obj.name;
        var newObj = Instantiate(obj);
        newObj.name = oldName;
        Destroy(obj);
        _attachedObject = newObj;
        _attachedObject.transform.SetParent(_gun.MuzzleTransform);

        _attachedObject.transform.localPosition = Vector3.zero;
        _attachedObject.transform.localRotation = Quaternion.Euler(0, 180, 0);

        ReverseGunView();
        onAttachedOnGun?.Invoke();
    }

    public GameObject AttachObjectOnSeat(
        Transform newParent,
        GameObject obj,
        bool hasObjAttached,
        Action OnAttachOnSeat)
    {
        if (hasObjAttached) return obj;
        if (!_attachedObject) return obj;

        var oldName = _attachedObject.name;
        var newObj = Instantiate(_attachedObject);
        newObj.name = oldName;

        Destroy(_attachedObject);
        obj = newObj;
        obj.transform.SetParent(newParent);

        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.Euler(0, 0, 0);

        OnAttachOnSeat?.Invoke();

        return obj;
    }

    public void ReverseObjFunction()
    {
        if (!_attachedObject) return;
        var functionInversable = 
            _attachedObject.GetComponentInChildren<IFunctionInversable>();
        if (functionInversable != null)
        {
            functionInversable.OnToggleFunctionInverse();
        }
    }

    public void ReverseGunView()
    {
        if (!_attachedObject) return;
        var objectIsReversed =
            _attachedObject.GetComponentInChildren<IFunctionInversable>().IsReversed;

        var gunInversable =
            _gun.GetComponent<IFunctionInversable>();
        if (gunInversable != null)
        {
            gunInversable.IsReversed = objectIsReversed;
            gunInversable.OnToggleFunctionInverse();
        }
    }
}
