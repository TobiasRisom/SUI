using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Movement : MonoBehaviour
{
    public float maxSpeed = 5.0f;
    public float currentSpeed = 0.0f;
    public float currentRotation = 0f;
    public float rotationSpeed = 1.6f;
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
            characterController.Move(moveDirection * currentSpeed * Time.deltaTime);
            
    }

    void DirectionCheck()
    {
        // Y-rotation for the controller  

        // First, find out what rotation the user wants (e.g. hard right)
        float controller_direction = rightController.transform.rotation.y;     

        // Then, make the current rotation direction the difference between what the user want and where they are going
        float rotation_direction = controller_direction - transform.rotation.y;

        // Then, rotate towards said direction
        transform.Rotate(0f, rotationSpeed * -rotation_direction, 0f);

    }
}