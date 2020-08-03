using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

/// <summary>
/// Script controlling the third person camera with cinemachine
/// </summary>
[RequireComponent(typeof(CinemachineFreeLook))]
public class ThirdPersonCamera : CinemachineInputProvider
{
    
    /// <summary>
    /// If true, the Y axis will control the camera orbit altitude
    /// </summary>
    [Header("Orbit altitude control")]
    [Tooltip("If true, the Y axis will control the camera orbit altitude")]
    public bool CanControlYAxis;

    /// <summary>Float action for Y movement, if null it will be the Y axis from the X axis reference</summary>
    [Tooltip("Float action for Y movement, if null it will be the Y axis from the X axis reference")]
    public InputActionReference YAxis;

    /// <summary>
    /// Implements the interface required by the cinemachine camera
    /// </summary>
    /// <param name="axis">Axis index ranges from 0...2 for X, Y, and Z.</param>
    /// <returns>The current axis value</returns>
    public override float GetAxisValue(int axis)
    {
        if (CanControlYAxis)
        {
            switch (axis)
            {
                case 1: // Y axis
                    if (YAxis != null) return YAxis.action.ReadValue<float>();
                    else return base.GetAxisValue(axis);
                default:
                    return base.GetAxisValue(axis);
            }
        }
        else
            switch (axis)
            {
                case 1: // Y axis disabled
                    return 0;
                default:
                    return base.GetAxisValue(axis);
            }
    }

}
