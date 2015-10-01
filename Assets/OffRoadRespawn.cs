using UnityEngine;
using System.Collections;

public class OffRoadRespawn : MonoBehaviour {

    Vector3 respawnPos = Vector3.zero;
    Rigidbody rigidbody = null;

	// Use this for initialization
	void Start () {
        respawnPos = transform.position;
        rigidbody = GetComponent<Rigidbody>();
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Road")
        {
            transform.position = respawnPos;
            rigidbody.velocity = Vector3.zero;
        }
    }

	// Update is called once per frame
	void Update () {
	
	}
}
