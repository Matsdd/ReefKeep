using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyfishMovementScript : MonoBehaviour
{
    // Variables to control the hovering effect
    public float hoverSpeed = 1.0f; // Speed of the hovering motion
    public float hoverAmplitude = 0.5f; // Amplitude of the hovering motion

    // Initial position of the jellyfish
    private Vector3 initialPosition;

    private void Start()
    {
        // Record the initial position of the jellyfish
        initialPosition = this.gameObject.transform.position;
    }

    private void FixedUpdate()
    {
        // Calculate the new position using a sine wave
        float newY = initialPosition.y + Mathf.Sin(Time.time * hoverSpeed) * hoverAmplitude;

        // Update the position of the jellyfish
        this.gameObject.transform.position = new Vector3(initialPosition.x, newY, initialPosition.z);
    }
}