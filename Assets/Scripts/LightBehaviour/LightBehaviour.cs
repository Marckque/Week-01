using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum LightState
{
    turnedOff = 0,
    turnedOn = 1
};

[RequireComponent(typeof(Light))]
public class LightBehaviour : MonoBehaviour
{
    [Header("Initial state"), SerializeField]
    private LightState m_InitialLightState;

    protected LightState m_LightState;
    protected Light CurrentLight { get; set; }

	protected void Awake()
    {
        CurrentLight = GetComponent<Light>();
        m_LightState = m_InitialLightState;
    }

    protected virtual void Start()
    {
        SetLightStatus();
    }

    protected void SetLightStatus()
    {
        CurrentLight.enabled = m_LightState == 0 ? false : true;
    }

    protected void SwitchLightState()
    {
        if (m_LightState == LightState.turnedOff)
        {
            m_LightState = LightState.turnedOn;
        }
        else
        {
            m_LightState = LightState.turnedOff;
        }

        SetLightStatus();
    }

    public void DeactivateLightBehaviour()
    {
        enabled = false;
    }
}