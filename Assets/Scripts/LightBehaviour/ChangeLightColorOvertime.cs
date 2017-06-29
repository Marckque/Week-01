using UnityEngine;
using System.Collections;

public class ChangeLightColorOvertime : LightBehaviour
{
    [SerializeField]
    private bool m_PauseBehaviourIfLightIsOff;

    [Header("Color overtime"), SerializeField]
    private Color[] m_Colors;
    [SerializeField, Range(0f, 10f)]
    private float m_TransitionDuration;

    private bool m_IsTransitioning;
    private float m_TransitionStart;
    private float m_TimeElapsed;
    private float m_CompletionPercentage;
    private float m_CustomTime;

    private int m_CurrentColorIndex = 0;
    private int m_NextColorIndex = 1;

    protected override void Start()
    {
        base.Start();

        StartColorChangement();
    }

    protected void Update()
    {
        if (m_PauseBehaviourIfLightIsOff)
        {
            if (CurrentLight.enabled == false)
            {
                return;
            }
        }

        if (m_IsTransitioning)
        {
            m_CustomTime += Time.deltaTime;
            m_TimeElapsed = m_CustomTime - m_TransitionStart;
            m_CompletionPercentage = m_TimeElapsed / m_TransitionDuration;

            CurrentLight.color = Color.Lerp(m_Colors[m_CurrentColorIndex], m_Colors[m_NextColorIndex], m_CompletionPercentage);

            if (m_CompletionPercentage >= 1)
            {
                m_IsTransitioning = false;

                m_TimeElapsed = 0f;
                m_CompletionPercentage = 0f;

                m_CurrentColorIndex++;
                m_NextColorIndex++;
                CheckIndexes();

                StartColorChangement();
            }
        }
    }

    private void StartColorChangement()
    {
        m_TransitionStart = m_CustomTime;
        m_IsTransitioning = true;
    }

    private void CheckIndexes()
    {
        if (m_CurrentColorIndex > m_Colors.Length - 1)
        {
            m_CurrentColorIndex = 0;
        }

        if (m_NextColorIndex > m_Colors.Length - 1)
        {
            m_NextColorIndex = 0;
        }
    }
}