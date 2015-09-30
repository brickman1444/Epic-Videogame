using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BearSpin))]
public class ButtonBearSpin : MonoBehaviour {

    [SerializeField] KeyCode spinKey;

    BearSpin bearSpin = null;

	// Use this for initialization
	void Start () {
        bearSpin = GetComponent<BearSpin>();
	}
	
	// Update is called once per frame
	void Update () {

        if (!bearSpin.spinning && Input.GetKeyDown(spinKey))
        {
            bearSpin.Spin();
        }
	}
}
