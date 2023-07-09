using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObject/Controllers/GravityController", 
    fileName = "GravityController")]
public class GravityController : ScriptableObject
{
    public UnityEvent<CharacterStatus> ChangeGravity;

    public bool PlayerControlOccupied = false;

    private void Awake()
    {
        ChangeGravity.AddListener(OnChangeGravity);
    }

    private void OnDestroy()
    {
        ChangeGravity.RemoveListener(OnChangeGravity);
    }

    private void OnChangeGravity(CharacterStatus status)
    {
        switch (status)
        {
            case CharacterStatus.Normal:
                PlayerControlOccupied = false;
                break;

            case CharacterStatus.TopOfGravityZone:
                PlayerControlOccupied = false;
                break;

            case CharacterStatus.InGravityZone:
                PlayerControlOccupied = true;
                break;

            case CharacterStatus.OnMovingPlatform:
                PlayerControlOccupied = true;
                break;
        }
    }
}
