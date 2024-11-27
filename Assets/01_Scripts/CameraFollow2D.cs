using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
     [Header("Target to Follow")]
    public Transform player; // Reference to the player's Transform

    [Header("Offset Settings")]
    public Vector3 offset = new Vector3(0, 2, -10); // Offset from the player's position

    [Header("Smoothness Settings")]
    [Range(0.01f, 1f)]
    public float smoothSpeed = 0.125f; // How smooth the camera moves (lower values = slower movement)

    private void LateUpdate()
    {
        if (player != null)
        {
            // Calculate the target position
            Vector3 targetPosition = player.position + offset;

            // Smoothly interpolate the camera's position towards the target position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            // Apply the smoothed position to the camera
            transform.position = smoothedPosition;
        }
    }
}
