using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour {

    [SerializeField,ReadOnly]
    bool isBeingDragged = false;
    [SerializeField]
    float cameraDistance = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (isBeingDragged)
        {
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cameraDistance));
        }
	}

    void OnMouseDown()
    {
        //Debug.Log("Mouse down");
        isBeingDragged = true;
    }

    void OnMouseUp()
    {
        //Debug.Log("Mouse up");
        isBeingDragged = false;
    }
}
