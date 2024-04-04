using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class moveTrigger : MonoBehaviour
{
    public GameObject lightCube;
    public bool isTouching = false;
    bool lightTest = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("rightController"))
        {
            lightCube.GetComponent<Renderer>().material.color = Color.green;
            Debug.Log("Entering!");
            isTouching = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("rightController"))
        {
            lightCube.GetComponent<Renderer>().material.color = Color.red;
            Debug.Log("Exiting!");
            isTouching = false;
        }
    }
}
