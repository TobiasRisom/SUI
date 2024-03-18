using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Movement : MonoBehaviour
{
    public float speed = 1.0f;
    private CharacterController characterController;
    public GameObject rightController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
            // Move the player in the direction that the controller is facing
            Vector3 moveDirection = rightController.transform.forward;
            //moveDirection.y = 0f;
            characterController.Move(moveDirection * speed * Time.deltaTime);
    }
}