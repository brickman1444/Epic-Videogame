using UnityEngine;
using System.Collections;

public class BearSpin : MonoBehaviour {

    [SerializeField] float spinSpeed = 0.0f;
    [SerializeField] KeyCode spinKey;

    bool spinning = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        if ( !spinning && Input.GetKeyDown(spinKey) )
        {
            Spin();
        }

	}

    void Spin()
    {
        StartCoroutine(SpinRoutine());
    }

    IEnumerator SpinRoutine()
    {
        spinning = true;
        float angle = 0;
        while (angle < 360)
        {
            angle += Time.deltaTime * spinSpeed;
            Debug.Log(angle);
            transform.Rotate(Vector3.forward, Time.deltaTime * spinSpeed);
            yield return null;
        }
        spinning = false;
    }
}
