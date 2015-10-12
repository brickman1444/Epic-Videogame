using UnityEngine;
using System.Collections;

public class ManualSwimmer : MonoBehaviour {

    [SerializeField] KeyCode leftButton;
    [SerializeField] KeyCode rightButton;

    Swim swim = null;

	// Use this for initialization
	void Start () {
        swim = GetComponent<Swim>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKey(leftButton))
        {
            swim.SwimLeft();
        }
        else if (Input.GetKey(rightButton) )
        {
            swim.SwimRight();
        }

	}
}
