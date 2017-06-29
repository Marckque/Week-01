using UnityEngine;

public class LightBlink : LightBehaviour
{
    [Header("Blink"), SerializeField, Range(0f, 10f)]
    private float m_DelayBetweenBlink; 
    private float m_LastBlinkTime;

    protected void Update()
    {
        if (m_LastBlinkTime + m_DelayBetweenBlink < Time.time)
        {
            m_LastBlinkTime = Time.time;
            SwitchLightState();
        }
    }
}