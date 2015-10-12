using UnityEngine;
using System.Collections;

public class DragAndDrop : MonoBehaviour {

    [SerializeField,ReadOnly]
    bool isBeingDragged = false;
    [SerializeField]
    float cameraDistance = 0.0f;
    [SerializeField]
    float snapTestDistance = 0.0f;

    const float snapTestMinDistanceBase = 0.51f;

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
        TryToSnap();
    }

    void TryToSnap()
    {
        RaycastHit2D outInfo = Physics2D.Raycast(transform.position + -transform.up * snapTestMinDistanceBase * transform.localScale.y, -transform.up, snapTestDistance);

        if (outInfo && outInfo.collider && outInfo.collider.gameObject)
        {
            Debug.Log(outInfo.collider.gameObject.name);
        }
    }
}
