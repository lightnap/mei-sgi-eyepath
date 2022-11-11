using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PauseManager : MonoBehaviour
{

    private InputDevice m_targetDeviceRight;
    private InputDevice m_targetDeviceLeft;

    private bool m_isPaused = false;
    private bool m_wasPausedLastFrame = false;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devicesRight = new List<InputDevice>();
        List<InputDevice> devicesLeft = new List<InputDevice>();
        InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, devicesRight);
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, devicesLeft);
        targetDeviceRight = devicesRight[0];
        targetDeviceLeft = devicesLeft[0];
    }

    // Update is called once per frame
    void Update()
    {
        // Check inputs (and timer) to activate pause state. 


        if (m_isPaused && !m_wasPausedLastFrame) 
        {
            // Enter pause.
        }

        if (m_isPaused && !m_wasPausedLastFrame) 
        {
            // Exit pause.
        }

        m_wasPausedLastFrame = m_isPaused; 
    }
}
