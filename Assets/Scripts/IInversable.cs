using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInversable
{
    void OnHitByReverseGunBullet();
}

public interface IGravityAppliable
{
    void OnGravityChanged();
}
