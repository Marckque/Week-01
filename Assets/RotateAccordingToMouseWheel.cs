using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAccordingToMouseWheel : MonoBehaviour
{
    protected void Update()
    {
        float heatingPowerAddition = Input.GetAxisRaw("Mouse ScrollWheel");

        if (heatingPowerAddition != 0f)
        {
            Vector3 newRotation = new Vector3(heatingPowerAddition * 20f, 0f, 0f);
            transform.eulerAngles += newRotation;
        }
    }
}