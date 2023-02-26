using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = Vector3.zero;
    void Update()
    {
        if (target == null) return;
        transform.position = target.transform.position + offset;
    }
}
