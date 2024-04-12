using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ForceMovement : MonoBehaviour
{
    public float speed = 5f;
    public float maxSpeed;
    public float acceleration = 0f;
    public float torque;
    public float angle;
    public float sv_normalized;
    public float maxTorque = 5f;
    public float torqueChangeSpeed = 2f;
    Rigidbody rb;
    public InputActionReference triggerAction;
    public InputActionReference gripAction;
    public Transform rightController;
    public Transform vectorGuide;
    public Transform parentBoat;
    bool triggerDown = false;
    bool gripDown = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {   
        // Get the states of buttons
        buttonStates();

        // Get the y-angle from the right controller
        angle = rightController.transform.localEulerAngles.y;

        // Convert values to lie between -180 to 180
        if(angle > 180)
        {
            angle -= 360;
        }

        // Clamp value to be between -90 and 90 
        angle = Mathf.Clamp(angle, -90f, 90f);

        // Change the vector if 
        if(triggerDown)
        {
        vectorGuide.localRotation = Quaternion.Euler(0, -angle, 0);
        }

        if(gripDown)
        {
            if(speed < maxSpeed)
            {
            acceleration += 15*Time.deltaTime;
            speed = speed+acceleration;
            }
        }
        else{
            acceleration -= 5*Time.deltaTime; 
            speed -= 4000*Time.deltaTime;
        }
    
        if(acceleration < 0)
        {
            acceleration = 0;
        }
               if(speed < 0)
        {
            speed = 0;
        }


        // ROTATION VALUE

        // Normalize the support to lie between -1 to 1
        sv_normalized = vectorGuide.localEulerAngles.y;
        sv_normalized = sv_normalized > 180 ? sv_normalized - 360 : sv_normalized;
        sv_normalized = sv_normalized / 90;
        torqueController();

    }

    void torqueController()
    {
        // Controls the current torque value
        torque += sv_normalized * torqueChangeSpeed;

        // Make sure we don't go over max values
        
        torque = torque > maxTorque ? maxTorque : torque;
        torque = torque < -maxTorque ? -maxTorque : torque;
    }
    void FixedUpdate()
    {
        // ACCELERATION
        Debug.Log($"Force being applied: {vectorGuide.forward * speed * Time.deltaTime}");

        rb.AddForce(vectorGuide.forward * speed * Time.deltaTime, ForceMode.Force);

        // ROTATION

        // Add the rotation around the Y-axis of the boat
        //rb.AddTorque(transform.up * torque, ForceMode.Force);
        //parentBoat.Rotate(Vector3.up * torque * Time.deltaTime);
        transform.Rotate(Vector3.up * torque * Time.deltaTime);
    }

    void LateUpdate()
    {
        // Preventing unneccessary position or rotation changes

        // Get the current rotation
        Quaternion currentRotation = transform.rotation;

        // Reset the rotation to zero
        transform.rotation = Quaternion.identity;

        // Preserve the y-axis rotation
        float newYRotation = currentRotation.eulerAngles.y;
        
        this.transform.position = new Vector3(this.transform.position.x, 0.38f, this.transform.position.z);
        transform.Rotate(0, newYRotation, 0);
        
    }

    void buttonStates()
    {
        // wow look how fancy we are we're using terniary operators oooo
        triggerDown = triggerAction.action.ReadValue<float>() > 0 ? true : false;
        gripDown = gripAction.action.ReadValue<float>() > 0 ? true : false;
    }
    void OnEnable()
    {
        triggerAction.action.Enable();
        triggerAction.action.performed += OnTriggerStarted;

        gripAction.action.Enable();
        gripAction.action.performed += OnGripStarted;
    }

    void OnDisable()
    {
        triggerAction.action.Disable();
        triggerAction.action.performed -= OnTriggerStarted;

        gripAction.action.Disable();
        gripAction.action.performed -= OnGripStarted;
    }

    void OnTriggerStarted(InputAction.CallbackContext context)
    {
        if (context.action == triggerAction.action)
        {
            Debug.Log("Right hand trigger pressed!");
        }
    }

    void OnGripStarted(InputAction.CallbackContext context)
    {
        if (context.action == gripAction.action)
        {
            Debug.Log("Right hand grip pressed!");
        }
    }
   
}

// Old Rotation Code
/*
        // Get the angle from the controller (0 to 360 degrees)
        angle = rightController.transform.rotation.eulerAngles.y;

        // Convert values to lie between -180 to 180
        if(angle > 180)
        {
            angle -= 360;
        }

        // Clamp value to be between -90 and 90
        angle = Mathf.Clamp(angle, -90f, 90f);

        // Normalize to -1 to 1
        float normalizedAngle = angle / 90;

        // Add the torque around the Y-axis of the boat
        rb.AddTorque(transform.up * torque * normalizedAngle * Time.deltaTime);

        */