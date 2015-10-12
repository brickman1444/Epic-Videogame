using UnityEngine;
using System.Collections;

public class Driving : MonoBehaviour {

    [SerializeField] Vector3 localForward = Vector3.zero;
    [SerializeField] float acceleration = 0.0f;
    [SerializeField] float maxSpeed = 0.0f;
    [SerializeField] float brakes = 0.0f;
    [SerializeField] float turningSpeed = 0.0f;

    [SerializeField] string gasAxis = "";
    [SerializeField] string turnAxis = "";

    new Rigidbody rigidbody = null;

	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 forward = transform.rotation * localForward;

        float gasInput = Input.GetAxis(gasAxis);

        if (gasInput > 0)
        {
            rigidbody.AddForce(forward * Time.deltaTime * acceleration);
        }
        else if (gasInput < 0)
        {
            rigidbody.velocity *= brakes;
        }

        if (rigidbody.velocity.magnitude > maxSpeed)
        {
            rigidbody.velocity = maxSpeed * rigidbody.velocity.normalized;
        }

        float turningInput = Input.GetAxis(turnAxis);

        transform.Rotate(-Vector3.forward, Time.deltaTime * turningSpeed * turningInput);

	}
}
