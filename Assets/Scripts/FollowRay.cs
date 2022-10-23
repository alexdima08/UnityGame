using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRay : MonoBehaviour
{
    public static Quaternion fixedRotation;
    private void Awake()
    {
        fixedRotation = transform.rotation;
    }

    private void LateUpdate()
    {
        //transform.rotation = fixedRotation;
        transform.rotation = Quaternion.Euler(fixedRotation.x, fixedRotation.y, fixedRotation.z);
    }
}
