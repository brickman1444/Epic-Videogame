using UnityEngine;
using System.Collections;

public class Escape : MonoBehaviour {

    [SerializeField]
    KeyCode quitKey;

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(quitKey))
        {
            Application.Quit();
        }
        
	}
}
