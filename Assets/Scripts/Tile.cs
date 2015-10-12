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
    float zPosition = 0.0f;
    [SerializeField]
    float grabbingDistance = 0.0f;
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
            transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, grabbingDistance));
        }
	}

    void OnMouseDown()
    {
        if (parent)
        {
            parent.child = null;
            parent = null;
        }
        if (child)
        {
            child.parent = null;
            child = null;
        }
        //Debug.Log("Mouse down");
        isBeingDragged = true;
    }

    void OnMouseUp()
    {
        //Debug.Log("Mouse up");
        isBeingDragged = false;
        transform.position = transform.position.SetZ(zPosition);
        TryToSnap();
    }

    void TryToSnap()
    {
        // Try to snap down
        RaycastHit2D downInfo = Physics2D.Raycast(transform.position + -transform.up * halfSize * transform.localScale.y, -transform.up, snapTestDistance);

        if (downInfo && downInfo.collider && downInfo.collider.gameObject)
        {
            Tile targetTile = downInfo.collider.GetComponent<Tile>();
            if (targetTile)
            {
                if (targetTile.parent == null)
                {
                    //Debug.Log(outInfo.collider.gameObject.name);
                    transform.position = downInfo.transform.position + transform.up * (1.0f * transform.localScale.y + snapMargin);
                    targetTile.parent = this;
                    child = targetTile;
                }
            }
        }
        else
        {
            // Try to snap up
            RaycastHit2D upInfo = Physics2D.Raycast(transform.position + transform.up * halfSize * transform.localScale.y, transform.up, snapTestDistance);

            if (upInfo && upInfo.collider && upInfo.collider.gameObject)
            {
                Tile targetTile = upInfo.collider.GetComponent<Tile>();
                if (targetTile)
                {
                    if (targetTile.child == null)
                    {
                        //Debug.Log(outInfo.collider.gameObject.name);
                        transform.position = upInfo.transform.position + -transform.up * (1.0f * transform.localScale.y + snapMargin);
                        targetTile.child = this;
                        parent = targetTile;
                    }
                }
            }
        }
    }
}
