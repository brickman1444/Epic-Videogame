using UnityEngine;
using System.Collections;

public class Driving : MonoBehaviour {

    [SerializeField] Vector3 localForward = Vector3.zero;
    [SerializeField] float acceleration = 0.0f;
    [SerializeField] float maxSpeed = 0.0f;
    [SerializeField] float friction = 0.0f;
    [SerializeField] float turningSpeed = 0.0f;

    float speed = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 forward = transform.rotation * localForward;

        float gasInput = Input.GetAxis("Vertical");

        if (gasInput > 0.01f || gasInput < -0.01f)
        {
            // Real input
            speed += Time.deltaTime * acceleration * gasInput;
            if (speed > maxSpeed)
            {
                speed = maxSpeed;
            }
            else if (speed < -maxSpeed)
            {
                speed = -maxSpeed;
            }
        }
        else
        {
            // no gas pedal
            if (speed > 0.01f || speed < -0.01f)
            {
                float frictionDirection = -speed / Mathf.Abs(speed);
                speed += Time.deltaTime * friction * frictionDirection;
            }
            else
            {
                speed = 0.0f;
            }
        }

        Debug.Log(speed);

        transform.position += Time.deltaTime * speed * forward;

        float turningInput = Input.GetAxis("Horizontal");

        transform.Rotate(-Vector3.forward, Time.deltaTime * turningSpeed * turningInput);

	}
}
