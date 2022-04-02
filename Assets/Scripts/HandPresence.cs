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

    /// <summary>Calls TryInitizlize on scene load</summary>
    void Start()
    {
        TryInitialize();
    }

    /// <summary>Initializes hand models</summary>
    void TryInitialize()
    {
        List<InputDevice> devices = new List<InputDevice>();

        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        // Log each device found
        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }

        // If device found
        if (devices.Count > 0)
        {
            // Set target device
            targetDevice = devices[0];
            // Choses corrosponding device's controller model prefab
            GameObject prefab = controllerPrefabs.Find(controller => controller.name == targetDevice.name);
            if (prefab)
            {
                spawnedController = Instantiate(prefab, transform);
            }
            else
            {
                Debug.Log("Did not find corresponding controller model");
            }
            // Instantiates hand model
            spawnedHandModel = Instantiate(handModelPrefab, transform);
            // Sets animator
            handAnimator = spawnedHandModel.GetComponent<Animator>();
        }
    }

    /// <summary>Animates hand models</summary>
    void UpdateHandAnimation()
    {
        if(targetDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
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

    /// <summary>Shows either hand model or controller depending on settings</summary>
    void Update()
    {
        // If target device not found, try to reinitialize it
        if(!targetDevice.isValid)
        {
            TryInitialize();
        }
        else
        {
            // If showController chosen, make hand model inactive and controller model active
            if (showController)
            {
                if(spawnedHandModel)
                    spawnedHandModel.SetActive(false);
                if(spawnedController)
                    spawnedController.SetActive(true);
            }
            // If showController not chosen, make hand model active and controller model inactive
            else
            {
                if (spawnedHandModel)
                    spawnedHandModel.SetActive(true);
                if (spawnedController)
                    spawnedController.SetActive(false);
                UpdateHandAnimation();
            }
        }
    }
}
