using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Character : MonoBehaviour
{

    private Rigidbody rb;

    [Header("Character Kart Speed Setting")]
    [SerializeField] private float initialSpeed = 10;
    [SerializeField] private float maxAngularSpeed = 2.0f;
    [SerializeField] private float maxSpeed = 200.0f;
    [SerializeField] private float rotationSpeed = 0.1f;
    [SerializeField] private float minSpeedForRotation = 0.1f;

    [Header("Character Kart Force Setting")]
    [SerializeField] private float handbrakeForce = 100.0f;


    // Start is called before the first frame update
    void Start()
    {
        // get the rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float speed = rb.velocity.magnitude;
        float torqueForce = 20.0f / (speed + 1.0f);
        
        // Move the kart character with add force and acceleration on the rigidbody with a max speed of 10
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * initialSpeed, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * initialSpeed / 1.5f, ForceMode.Acceleration);
        }


        // Rotate the kart character with add torque on the rigidbody
        if (speed > minSpeedForRotation)
        {
            if (Input.GetKey(KeyCode.A))
            {
                // Applique un couple pour faire tourner le kart à gauche
                float targetTorque = torqueForce;
                float currentTorque = Mathf.Lerp(0, targetTorque, rotationSpeed);
                Vector3 torque = Vector3.down * currentTorque;
                rb.AddTorque(torque, ForceMode.VelocityChange);
            }
            if (Input.GetKey(KeyCode.D))
            {
                // Applique un couple pour faire tourner le kart à droite
                float targetTorque = torqueForce;
                float currentTorque = Mathf.Lerp(0, targetTorque, rotationSpeed);
                Vector3 torque = Vector3.up * currentTorque;
                rb.AddTorque(torque, ForceMode.VelocityChange);
            }
        }


        // Handbrake to stop the kart
        if (Input.GetKey(KeyCode.Space))
        {
            float handbrakeForce = 100.0f;  // Adjust this value to change the strength of the handbrake
            Vector3 handbrakeDirection = -rb.velocity.normalized;
            rb.AddForce(handbrakeDirection * handbrakeForce, ForceMode.Acceleration);
        }

        // Limit the angular speed of the kart
        if (rb.angularVelocity.magnitude > maxAngularSpeed)
        {
            rb.angularVelocity = rb.angularVelocity.normalized * maxAngularSpeed;
        }

        //Dead zone for the torque to avoid the kart to turn when no input is pressed
        if (!Input.GetKeyUp(KeyCode.A) && !Input.GetKeyUp(KeyCode.D))
        {
            rb.angularVelocity = Vector3.zero;
        }

        // Limit the speed of the kart
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

        // Slow down the kart when no input is pressed
        if (rb.velocity.magnitude > 0)
        {
            rb.velocity *= 0.999f;
        }

    }
}
