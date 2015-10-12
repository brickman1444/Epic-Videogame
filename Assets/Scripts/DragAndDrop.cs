using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour {

    [SerializeField,ReadOnly]
    bool isBeingDragged = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnMouseDown()
    {
        Debug.Log("Mouse down");
        isBeingDragged = true;
    }

    void OnMouseUp()
    {
        Debug.Log("Mouse up");
        isBeingDragged = false;
    }
}
