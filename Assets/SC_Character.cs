using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SC_Character : MonoBehaviour
{

    private Rigidbody rb;

    [Header("Character Kart Setting")]
    [SerializeField]
    private float initialSpeed = 10;

    // Start is called before the first frame update
    void Start()
    {
        // get the rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
        // Move the kart character with add force and acceleration on the rigidbody with a max speed of 10
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * initialSpeed, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * initialSpeed/1.5f, ForceMode.Acceleration);
        }

        if (Input.GetKey(KeyCode.A))
        {
            //rotate the kart to the left smoothly
            transform.Rotate(Vector3.down * 0.5f);

        }
        if (Input.GetKey(KeyCode.D))
        {
            //rotate the kart to the right smoothly
            transform.Rotate(Vector3.up * 0.5f);
        }

        if (rb.velocity.magnitude > 0)
        {
            rb.velocity *= 0.999f;
        }

    }
}
