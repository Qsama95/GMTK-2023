using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObject/Containers/ReverseGunBulletContainer",
    fileName = "ReverseGunBulletContainer")]
public class ReverseGunBulletContainer : ScriptableObject
{
    [SerializeField] private GameObject _bullet;

    public GameObject SpawnBullet(Vector3 pos)
    {
        return Instantiate(_bullet, pos, Quaternion.identity);
    }
}
