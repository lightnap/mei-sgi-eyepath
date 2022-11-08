using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR; 

public class HandPresence : MonoBehaviour
{
    public bool showController = false; 
    public InputDeviceCharacteristics controllerCharacteristics; 
    public List<GameObject> controllerPrefabs;
    public GameObject handModelPrefab; 

    private InputDevice targetDevice;
    private GameObject spawnedController;
    private GameObject spawnedHandModel;

    private Animator handAnimator; 

    // Start is called before the first frame update
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
           // Debug.Log(item.name + "//" + item.characteristics);
        }

        if (devices.Count > 0)
        {
            targetDevice = devices[0];

            string LeftOrRightControllerNameModifier = "";
            if (controllerCharacteristics == (InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller)) 
            {
                LeftOrRightControllerNameModifier = " - Left";
            }
            if (controllerCharacteristics == (InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller)) 
            {
                LeftOrRightControllerNameModifier = " - Right";
            }


            GameObject prefab = controllerPrefabs.Find(controllerPrefabs => controllerPrefabs.name == (targetDevice.name + LeftOrRightControllerNameModifier));
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                // If we don't find controller, set to a default.
                Debug.LogError("Did not find corresponding controller model " + targetDevice.name + LeftOrRightControllerNameModifier);
                spawnedController = Instantiate(controllerPrefabs[0], transform);
            }

            spawnedHandModel = Instantiate(handModelPrefab, transform);
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    void UpdateHandAnimation() 
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else 
        {
            handAnimator.SetFloat("Trigger", 0);
        }
        if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!targetDevice.isValid)
        {
            TryInitialize();
        }
        else 
        {
            if (showController)
            {
                spawnedHandModel.SetActive(false);
                spawnedController.SetActive(true);
            }
            else
            {
                spawnedHandModel.SetActive(true);
                spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }
    }

    void DebugInputs() 
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonValue))
        {
            if (primaryButtonValue)
            {
                Debug.Log("Pressing Primary Button");
            }
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            if (triggerValue > 0.1f)
            {
                Debug.Log("TriggerPressed " + triggerValue);
            }
        }

        if (targetDevice.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 primary2DAxisValue))
        {
            if (primary2DAxisValue != Vector2.zero)
            {
                Debug.Log("PrimaryTouchpad" + primary2DAxisValue);
            }
        }
    }
}
