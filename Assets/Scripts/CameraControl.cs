using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    bool axisEnabled = true;

    public static CameraControl instance;

    void Start()
    {
        if (instance != null)
        {
            Debug.LogError("More than one CameraControl in scene");
        }
        else
        {
            instance = this;
        }

        CinemachineCore.GetInputAxis = GetAxisCustom;
    }

    public float GetAxisCustom(string axisName)
    {
        if (!axisEnabled)
            return 0;
        return Input.GetAxis(axisName);
    }

    public void EnableAxis(bool cond, string axisName)
    {
        Debug.Log("Enable Axis");
        axisEnabled = cond;
        GetAxisCustom(axisName);
    }
}
