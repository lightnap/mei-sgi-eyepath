using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR; 

/// <summary>
/// Toggle between the direct and ray interactor if the direct interactor isn't touching any objects
/// Should be placed on a ray interactor
/// </summary>
[RequireComponent(typeof(XRRayInteractor))]
public class ToggleRay : MonoBehaviour
{
    [Tooltip("Switch even if an object is selected")]
    public bool forceToggle = false;

    [Tooltip("The direct interactor that's switched to")]
    public XRDirectInteractor directInteractor = null;

    public InputDeviceCharacteristics controllerCharacteristics;

    public float AxisToPressThreshold  = 0.0f; 


    private XRRayInteractor rayInteractor = null;
    private bool isSwitched = false;
    private InputDevice targetDevice;
    //private GameObject reticle; 

    private void Awake()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
        //reticle = GetComponent<XRInteractorLineVisual>().reticle; 
        SwitchInteractors(false);
    }

    void Start()
    {
        TryInitialize();
    }

    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        foreach (var item in devices)
        {
             Debug.Log(item.name + "//" + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    void Update() 
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue)) // Only if device has such input. 
        {

            if (primary2DAxisValue.y >= AxisToPressThreshold &&  !isSwitched)
            {
                ActivateRay();
            }
            if (primary2DAxisValue.y <= AxisToPressThreshold && isSwitched )
            {
                DeactivateRay(); 
            }
        }
    }

    public void ActivateRay()
    {
        if (!TouchingObject() || forceToggle)
            SwitchInteractors(true);
    }

    public void DeactivateRay()
    {
        if (isSwitched)
            SwitchInteractors(false);
    }

    private bool TouchingObject()
    {
        List<IXRInteractable> targets = new List<IXRInteractable>();
        directInteractor.GetValidTargets(targets);
        return (targets.Count > 0);
    }

    private void SwitchInteractors(bool value)
    {
        isSwitched = value;
        rayInteractor.enabled = value;
        directInteractor.enabled = !value;
        //if (reticle) 
       // {
           // reticle.SetActive(value); 
       // }
    }
}
