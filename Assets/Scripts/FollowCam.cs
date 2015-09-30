using UnityEngine;
using System.Collections;

public class FollowCam : MonoBehaviour {

    [SerializeField] GameObject target;
    [SerializeField] float maxDistance = 0.0f;
    [SerializeField] float zDistance = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 difference = transform.position - target.transform.position;

        difference.z = 0;

        if (difference.magnitude > maxDistance)
        {
            difference.Normalize();
            difference *= maxDistance;

            difference.z = -1 * zDistance;

            transform.position = target.transform.position + difference;
        }

	}
}
