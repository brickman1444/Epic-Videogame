using UnityEngine;
using System.Collections;

public class AxisMovement : MonoBehaviour {

    [SerializeField] float speed = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        float deltaX = Time.deltaTime * speed * Input.GetAxis("Horizontal");
        float deltaY = Time.deltaTime * speed * Input.GetAxis("Vertical");

        transform.position += new Vector3(deltaX, deltaY, 0.0f);

	}
}
