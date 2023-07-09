using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GravityEmitter : MonoBehaviour, IHitAttachable
{
    [SerializeField] private ReverseGunController _gunController;
    [SerializeField] private GameObject _gravityApplier;

    public bool HasGravityApplier => GravityApplier;

    public GameObject GravityApplier { get => _gravityApplier; set => _gravityApplier = value; }

    public UnityEvent KidnapGravityApplier;
    public UnityEvent ReturnGravityApplier;

    private void Start()
    {
        Init();
    }

    private void Init()
    {

    }

    public void OnHitByReverseGunBullet()
    {
        // check if emitter has gravity applier or not
        if (HasGravityApplier)
        {
            // set it on reverse gun
            _gunController.AttachObjectOnGun(
                GravityApplier,
                () => {
                    KidnapGravityApplier?.Invoke();
                });
        }
        else
        {
            // set in on emitter
            _gravityApplier = _gunController.AttachObjectOnSeat(
                transform,
                GravityApplier,
                HasGravityApplier,
                () => {
                    ReturnGravityApplier?.Invoke();
                });
        }
    }
}
