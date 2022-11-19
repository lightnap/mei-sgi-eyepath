using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR;

public class PointRayToVision : MonoBehaviour
{
   
    public enum eSides
    {
        Left,
        Right
    }

    public eSides mCurrentSide = eSides.Left;  

    
 
    // Other.
    private InputDevice m_targetDeviceHand;
    private InputDevice m_targetDeviceEye;

    public Transform XRrig = null; 


    // Start is called before the first frame update
    void Start()
    {
        TryInitialize(); 
    }

    void TryInitialize()
    {
        List<InputDevice> devicesEye = new List<InputDevice>();
        List<InputDevice> devicesHand = new List<InputDevice>();

        InputDeviceCharacteristics handControllerCharacteristics = InputDeviceCharacteristics.Controller;
        InputDeviceCharacteristics eyeControllerCharacteristics = InputDeviceCharacteristics.HeadMounted;
        
        if (mCurrentSide == eSides.Left)
        {
            handControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        }
        else if (mCurrentSide == eSides.Right)
        {
            handControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        }

        InputDevices.GetDevicesWithCharacteristics(eyeControllerCharacteristics, devicesEye);
        InputDevices.GetDevicesWithCharacteristics(handControllerCharacteristics, devicesHand);
        if (devicesEye.Count > 0 && devicesHand.Count > 0)
        {
            m_targetDeviceHand = devicesHand[0];
            m_targetDeviceEye = devicesEye[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_targetDeviceHand.isValid || !m_targetDeviceEye.isValid)
        {
            TryInitialize();
        }
        else 
        {
            if (mCurrentSide == eSides.Left && m_targetDeviceEye.TryGetFeatureValue(CommonUsages.leftEyePosition, out Vector3 leftEyePosition))
            {
                UpdateOrientation(leftEyePosition);
            }
            if (mCurrentSide == eSides.Right && m_targetDeviceEye.TryGetFeatureValue(CommonUsages.rightEyePosition, out Vector3 rightEyePosition))
            {
                UpdateOrientation(rightEyePosition);
            }
        } 
    }

    void UpdateOrientation(Vector3 eyePosition)
    {
        if (m_targetDeviceHand.TryGetFeatureValue(CommonUsages.devicePosition, out Vector3 handPosition))
        {
            Vector3 DirectionVector = new Vector3 (1.0f,0.0f,0.0f);
            DirectionVector = handPosition - eyePosition;
            DirectionVector.Normalize(); 
            
            // Add rotation of rig (due to snap turns) 
            float RotationY = XRrig.eulerAngles.y;

            DirectionVector = Quaternion.AngleAxis(RotationY, Vector3.up) * DirectionVector;
            transform.LookAt(transform.position + DirectionVector);
        }
    }

    public void SetLeftDirection()
    {
        mCurrentSide = eSides.Left; 
    }

    public void SetRightDirection()
    {
        mCurrentSide = eSides.Right; 
    }
}
