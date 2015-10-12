using UnityEngine;
using System.Collections;

public class OffRoadRespawn : MonoBehaviour {

    Vector3 respawnPos = Vector3.zero;
    Quaternion respawnRot = Quaternion.identity;
    new Rigidbody rigidbody = null;

	// Use this for initialization
	void Start () {
        respawnPos = transform.position;
        respawnRot = transform.rotation;
        rigidbody = GetComponent<Rigidbody>();
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Road")
        {
            transform.position = respawnPos;
            transform.rotation = respawnRot;
            rigidbody.velocity = Vector3.zero;
        }
    }
}
