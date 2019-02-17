using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtObject : MonoBehaviour
{
    public Transform target;

    private void Update()
    {
        transform.LookAt(transform.position + target.rotation * Vector3.forward,
        target.rotation * Vector3.up);
    }
}
