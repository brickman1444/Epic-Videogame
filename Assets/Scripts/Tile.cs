using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

    [SerializeField,ReadOnly]
    bool isBeingDragged = false;
    [SerializeField, ReadOnly]
    Tile parent = null;
    [SerializeField, ReadOnly]
    Tile child = null;
    [SerializeField]
    float cameraDistance = 0.0f;
    [SerializeField]
    float snapTestDistance = 0.0f;
    [SerializeField]
    float snapMargin = 0.0f;

    const float halfSize = 0.51f;

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
        if (parent)
        {
            parent.child = null;
        }
        if (child)
        {
            child.parent = null;
        }
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
        RaycastHit2D outInfo = Physics2D.Raycast(transform.position + -transform.up * halfSize * transform.localScale.y, -transform.up, snapTestDistance);

        if (outInfo && outInfo.collider && outInfo.collider.gameObject)
        {
            Tile targetTile = outInfo.collider.GetComponent<Tile>();
            if (targetTile)
            {
                if (targetTile.parent == null)
                {
                    //Debug.Log(outInfo.collider.gameObject.name);
                    transform.position = outInfo.transform.position + transform.up * (1.0f * transform.localScale.y + snapMargin);
                    targetTile.parent = this;
                    child = targetTile;
                }
            }
        }
    }
}
