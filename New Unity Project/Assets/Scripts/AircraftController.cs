using System;
using UnityEngine;

public class AircraftController : MonoBehaviour {
    // Constant forward thrust from the aircraft engines.
    public float forwardThrustForce = 40.0f;

    // Turning force as a multiple of the thrust force.
    public float turnForceMultiplier = 5000.0f;

    public float maxSpeed = 400.0f;

    private Vector3 controlForce;
    private Rigidbody rigidBody;

	void Start () 
    {
        rigidBody = GetComponent<Rigidbody>();
	}

	void Update () 
    {
        controlForce.Set(
            Input.GetAxis("Horizontal") * turnForceMultiplier, 
            Input.GetAxis("Vertical") * turnForceMultiplier, 
            1.0f
        );
        controlForce = controlForce.normalized * forwardThrustForce;
    }

    void FixedUpdate()
    {
        float excessSpeed = Math.Max(0, rigidBody.velocity.magnitude - maxSpeed);        
        Vector3 brakeForce = rigidBody.velocity.normalized * excessSpeed;    
        rigidBody.AddForce(-brakeForce, ForceMode.Force);
        if (transform.rotation.z < 20 && controlForce.y > 1)
        {
            transform.Rotate(0, 0, 0.5f);
        }
        if (transform.rotation.z > -20 && controlForce.y < 1)
        {
            transform.Rotate(0, 0, -0.5f);
        }

        rigidBody.AddRelativeForce(controlForce, ForceMode.Force);

        //transform.Rotate(0, 0, 0);

        transform.forward = rigidBody.velocity;

    }
}
    