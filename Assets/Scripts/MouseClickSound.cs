using UnityEngine;
using System.Collections;

public class MouseClickSound : MonoBehaviour {


    [SerializeField]
    AudioSource mouseDownAudio;
    [SerializeField]
    AudioSource mouseUpAudio;

	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            mouseDownAudio.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseUpAudio.Play();
        }
	}
}
