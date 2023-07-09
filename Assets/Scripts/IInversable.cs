using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHitAttachable
{
    void OnHitByReverseGunBullet();
}

public interface IFunctionInversable
{
    void OnToggleFunctionInverse();
}

public interface IGravityAppliable
{
    void OnGravityChanged();

    bool IsPlayerOnIt { get; set; }
}
