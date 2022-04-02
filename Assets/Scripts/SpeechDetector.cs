using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SpeechDetector : MonoBehaviour
{
    public Transform head;
    public Transform feet;
    void Update()
    {
        gameObject.transform.position = new Vector3(head.position.x, feet.position.y, head.position.z);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SpeechSpot")
        {
            other.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "SpeechSpot")
        {
            other.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
