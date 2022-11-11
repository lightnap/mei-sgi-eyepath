using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class PauseManager : MonoBehaviour
{
    enum eStates 
    {
        Running,
        Pausing,
        Paused
    }


    public XRDirectInteractor m_leftDirectInteractor = null;
    public XRDirectInteractor m_rightDirectInteractor = null;

    public GameObject m_xrayInteractorLeft = null;
    public GameObject m_xrayInteractorRight = null;

    public AudioSource pausedSound = null; 

    public float m_timeToStartPause = 1.0f; // Time it takes to start pause once you press the button. 

    public float m_MaxFogDensity = 0.5f;
    private float m_CurrentFogDenisity = 0.0f; 

    private InputDevice m_targetDeviceRight;
    private InputDevice m_targetDeviceLeft;

    private bool m_leftButtonPressed = false;
    private bool m_rightButtonPressed = false; 
    private bool m_anyButtonPressed = false;

    private eStates m_currentState = eStates.Running;
    private bool m_isPauseExitable = false;
    private bool m_isPauseEnterable = false; 

    private float m_startPauseTimer;

    // Start is called before the first frame update
    void Start()
    {
        TryInitialize(); 
    }

    private void TryInitialize() 
    {
        List<InputDevice> devicesRight = new List<InputDevice>();
        List<InputDevice> devicesLeft = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devicesRight);
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devicesLeft);
        if (devicesRight.Count > 0 && devicesLeft.Count > 0)
        {
            m_targetDeviceRight = devicesRight[0];
            m_targetDeviceLeft = devicesLeft[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_targetDeviceRight.isValid || !m_targetDeviceLeft.isValid)
        {
            TryInitialize();
        }
        else 
        {
            CheckInputs();
            StateMachine();
        } 
    }


    private void CheckInputs()
    {
        m_anyButtonPressed = false;
        m_leftButtonPressed = false; 
        m_rightButtonPressed = false; 

        // Check inputs (and timer) to activate pause state. 
        if (m_targetDeviceRight.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightPrimaryButtonValue))
        {
            m_rightButtonPressed =  rightPrimaryButtonValue;
        }
        if (m_targetDeviceLeft.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftPrimaryButtonValue))
        {
            m_leftButtonPressed = leftPrimaryButtonValue;
        }
        if (!TouchingObject(m_leftDirectInteractor)) 
        {
            m_anyButtonPressed = m_anyButtonPressed || m_leftButtonPressed;
        }
        if (!TouchingObject(m_rightDirectInteractor))
        {
            m_anyButtonPressed = m_anyButtonPressed || m_rightButtonPressed;
        }
    }

    private bool TouchingObject(XRDirectInteractor directInteractor)
    {
        List<IXRInteractable> targets = new List<IXRInteractable>();
        directInteractor.GetValidTargets(targets);
        return (targets.Count > 0);
    }

    private void StateMachine() 
    {
        switch (m_currentState)
        {
            case eStates.Running:

                if (!m_anyButtonPressed) 
                {
                    m_isPauseEnterable = true; 
                }

                Debug.Log("Running");
                EnableComponents(true);

                m_CurrentFogDenisity = 0.0f; 

                m_startPauseTimer = m_timeToStartPause;
                if (m_anyButtonPressed && m_isPauseEnterable)
                {
                    m_currentState = eStates.Pausing;
                    m_isPauseEnterable = false; 
                }
                break;

            case eStates.Pausing:
                Debug.Log("Pausing");
                m_startPauseTimer -= Time.deltaTime;

                m_CurrentFogDenisity = m_MaxFogDensity * (m_timeToStartPause - m_startPauseTimer) / m_timeToStartPause; 
                if (!m_anyButtonPressed)
                {
                    m_currentState = eStates.Running;
                }
                else if (m_startPauseTimer <= 0)
                {
                    m_currentState = eStates.Paused;
                    m_startPauseTimer = m_timeToStartPause;
                    m_isPauseExitable = false;
                }
                break;

            case eStates.Paused:
                Debug.Log("Paused");

                m_CurrentFogDenisity = m_MaxFogDensity;
                EnableComponents(false); 

                if (!m_anyButtonPressed)
                {
                    m_isPauseExitable = true;
                }
                if (m_anyButtonPressed && m_isPauseExitable)
                {
                    m_currentState = eStates.Running;
                    m_isPauseExitable = false;
                }
                break;
        }

        RenderSettings.fogDensity = m_CurrentFogDenisity; 
    }

    void EnableComponents(bool aEnable) 
    {
        m_xrayInteractorLeft.SetActive(aEnable);
        m_xrayInteractorRight.SetActive(aEnable);
        if (aEnable)
        {
            m_leftDirectInteractor.enabled = true;
            m_rightDirectInteractor.enabled = true;
        }
        else
        {
            if (!TouchingObject(m_leftDirectInteractor)) 
            {
                m_leftDirectInteractor.enabled = false;
            }
            if (!TouchingObject(m_rightDirectInteractor)) 
            {
                m_rightDirectInteractor.enabled = false;
            }
        }
    }
}
