using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<IGravityAppliable>() != null)
        {
            other.GetComponent<IGravityAppliable>().IsPlayerOnIt = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<IGravityAppliable>() != null)
        {
            other.GetComponent<IGravityAppliable>().IsPlayerOnIt = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<IGravityAppliable>() != null)
        {
            other.GetComponent<IGravityAppliable>().IsPlayerOnIt = false;
        }
    }
}
