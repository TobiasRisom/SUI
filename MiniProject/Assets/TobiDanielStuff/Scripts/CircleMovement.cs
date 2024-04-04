using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public Transform Cube;
    public float radius = 50f; // Radius of the circular path

    private CharacterController characterController;
    public GameObject rightController;

    private float angle;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        // Increment angle based on speed
        angle = Mathf.Clamp(rightController.transform.rotation.eulerAngles.y, -90f, 90f);

        // Calculate the position around the circle using trigonometry
        float x = transform.position.x + radius * Mathf.Cos(angle);
        float z = transform.position.z + radius * Mathf.Sin(angle);

        // Set the position of the object
        Cube.position = new Vector3(x, Cube.position.y, z);

        Debug.Log(angle);
    }
}

