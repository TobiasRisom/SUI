using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Movement : MonoBehaviour
{
    public float maxSpeed = 5.0f;
    public float currentSpeed = 0.0f;
    public float currentRotationLevel = 0f;
    public float rotationSpeed;
    public Vector3 moveDirection;
    private CharacterController characterController;
    public GameObject rightController;
    public GameObject boat;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
            moveDirection = transform.forward;
            //moveDirection.y = 0f;
            DirectionCheck();
            //characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
            
    }

    void DirectionCheck()
    {
        // Y-rotation for the controller  
/*
        // First, find out what rotation the user wants (e.g. hard right)
        controller_direction = rightController.transform.rotation.y;   

        // Then, make the current rotation direction the difference between what the user want and where they are going
        rotation_direction = controller_direction - transform.rotation.y;

        rotation = -rotation_direction;

        // Then, rotate towards said direction
        transform.Rotate(0f, rotation, 0f);
        */

        // First, find the user rotation (clamped to 90 degreen turns)
        rotationSpeed = Mathf.Clamp(rightController.transform.rotation.eulerAngles.y, -90f, 90f);

        // Next, we want to compare the current rotation level with the user rotation.
        // If they match, great! Means we are rotating as the user wants.
        // If NOT, we need the boat rotation to adjust to match the user rotation.
        // The speed of this change should be greater if the user rotation is higher

        // The max rotationlevel is 15, which is the controllers max value / 6:

        /*if(controller_direction < 0)
        {
            if(controller_direction / 6 < currentRotationLevel)
            {
                // Examples:
                // rotationspeed = -10 + -5 = -5
                // rotationspeed = -5 + -5 = 0
                // rotationspeed = 20 + -5 = 15
                // As the controller differes more from the current direction, the rotation speed increases
                rotationSpeed = controller_direction / 6 + currentRotationLevel;
            }
            else
        }*/
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);

    }
}