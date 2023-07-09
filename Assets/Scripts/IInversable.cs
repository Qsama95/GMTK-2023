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

    bool IsReversed { get; set; }
}

public interface IGravityAppliable : IGravityAppliableBase
{
    bool IsPlayerOnIt { get; set; }
}

public interface IGravityAppliableBase
{
    void OnGravityReleased();
}
