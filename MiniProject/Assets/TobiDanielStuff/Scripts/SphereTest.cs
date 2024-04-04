using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereTest : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public Transform rightController; // Reference to the right controller object
    public float orbitRadius = 50f; // Radius of the orbit
    public float heightOffset = 0f; // Vertical offset from the player
    public float rotationSpeed = 50f; // Speed of rotation around the player

    private void Update()
    {
        // Get the current rotation of the right controller
        float controllerRotationY = rightController.rotation.eulerAngles.y;

        // Calculate the position on the circle based on controller rotation
        float xPos = player.position.x + orbitRadius * Mathf.Sin(controllerRotationY * Mathf.Deg2Rad);
        float zPos = player.position.z + orbitRadius * Mathf.Cos(controllerRotationY * Mathf.Deg2Rad);

        // Set the position of the sphere
        transform.position = new Vector3(xPos, player.position.y + heightOffset, zPos);

        // Ensure that the sphere always faces towards the player
        transform.LookAt(player);

        // If you want the sphere to rotate around the player continuously,
        // you can uncomment the following line
        //transform.RotateAround(player.position, Vector3.up, rotationSpeed * Time.deltaTime);
    }
}