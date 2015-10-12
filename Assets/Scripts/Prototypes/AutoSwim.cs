using UnityEngine;
using System.Collections;

public class AutoSwim : MonoBehaviour {

    [SerializeField] float sideSwimTime = 0.0f;

    float startSwimTime = 0.0f;

    bool isSwimmingLeft = true;

    Swim swim = null;

	// Use this for initialization
	void Start () {
        startSwimTime = Time.time;
        swim = GetComponent<Swim>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time - startSwimTime > sideSwimTime)
        {
            isSwimmingLeft = !isSwimmingLeft;
            startSwimTime = Time.time;
        }

        if (isSwimmingLeft)
        {
            swim.SwimLeft();
        }
        else
        {

            swim.SwimRight();
        }

	}
}
