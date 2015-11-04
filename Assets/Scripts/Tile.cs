using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

    public enum Key
    {
        Invalid = 0,
        Start,
        Goal,
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
    }

    public enum TileType
    {
        Invalid = 0,
        Start,
        Goal,
        Middle,
    }

    [SerializeField,ReadOnly]
    bool isBeingDragged = false;
    [SerializeField, ReadOnly]
    Tile leftParent = null;
    [SerializeField, ReadOnly]
    Tile rightParent = null;
    [SerializeField, ReadOnly]
    Tile child = null;
    [SerializeField, ReadOnly]
    TileType tileType = TileType.Invalid;
    [SerializeField]
    float zPosition = 0.0f;
    [SerializeField]
    float grabbingDistance = 0.0f;
    [SerializeField]
    float snapTestDistance = 0.0f;
    [SerializeField]
    float snapMargin = 0.0f;
    [SerializeField]
    List<Key> _topKeys = new List<Key>();
    [SerializeField]
    Key _bottomKey = Key.Invalid;
    [SerializeField]
    GameObject goalEffect = null;
    [SerializeField]
    Material startTileMaterial = null;
    [SerializeField]
    Material goalTileMaterial = null;
    [SerializeField]
    GameObject twoTileSupportsEffect = null;
    [SerializeField]
    GameObject oneTileSupportEffect = null;
    [SerializeField]
    float idleLineVerticalDistance = 0.0f;
    [SerializeField]
    float idleLineHorizontalDistance = 0.0f;
    [SerializeField]
    Vector3 debugLineOffset = Vector3.zero;
    [SerializeField]
    float debugLineDuration = 0.0f;

    LineRenderer[] lineRenderers = new LineRenderer[2];

    const float halfSize = 0.51f;

    public bool isStartTile
    {
        get { return tileType == TileType.Start; }
    }

    public bool hasSingleParent
    {
        get { return topKeys.Count == 1; }
    }

    public bool hasOpenParentSlot
    {
        get
        {
            if (hasSingleParent)
            {
                return leftParent == null;
            }
            else
            {
                return leftParent == null || rightParent == null;
            }
        }
    }

    public List<Key> topKeys
    {
        get { return _topKeys; }
        private set { _topKeys = value; }
    }

    public Key bottomKey
    {
        get { return _bottomKey; }
        private set { _bottomKey = value; }
    }

	// Use this for initialization
	void Start () {
        lineRenderers = GetComponentsInChildren<LineRenderer>();
        ResetLines();
	}

    public void Initialize(string topKeyString, string bottomKeyString, string text)
    {
        foreach (string keyString in topKeyString.Split(',') )
        {
            topKeys.Add((Key)Enum.Parse(typeof(Key), keyString, true));
        }

        bottomKey = (Key)Enum.Parse(typeof(Key), bottomKeyString, true);

        if (bottomKey == Key.Goal)
        {
            MakeGoalTile();
        }

        if (topKeys.Count == 1 && topKeys[0] == Key.Start)
        {
            MakeStartTile();
        }

        if (bottomKey == Key.Start)
        {
            Debug.LogError("Bottom key set to Start");
        }

        if (topKeys.Contains(Key.Goal))
        {
            Debug.LogError("Top key set to Goal");
        }

        if (topKeys.Count == 1)
        {
            if (!isStartTile)
            {
                oneTileSupportEffect.SetActive(true);
            }
        }
        else if (topKeys.Count == 2)
        {
            twoTileSupportsEffect.SetActive(true);
        }
        else
        {
            Debug.LogError("Unexpected number of top keys: " + topKeys.Count + " from string: " + topKeyString);
        }

        Text uiText = GetComponentInChildren<Text>();
        if (uiText)
        {
            uiText.text = text;
        }
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
        RemoveAllParents();

        if (child)
        {
            child.RemoveAParent(this);
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

        LineDrawing.DrawLine(transform.position + debugLineOffset, transform.position + -transform.up * halfSize * transform.localScale.y + debugLineOffset, debugLineDuration);

        if (downInfo && downInfo.collider && downInfo.collider.gameObject)
        {
            Tile targetTile = downInfo.collider.GetComponent<Tile>();
            if (targetTile)
            {
                LineDrawing.DrawCollider(downInfo.collider, debugLineDuration);

                if (targetTile.hasOpenParentSlot && !targetTile.isStartTile && bottomKey != Key.Goal)
                {
                    targetTile.ConnectFromAbove(this);
                }
            }
        }
        else
        {
            // Try to snap up
            RaycastHit2D upInfo = Physics2D.Raycast(transform.position + transform.up * halfSize * transform.localScale.y, transform.up, snapTestDistance);

            LineDrawing.DrawLine(transform.position + debugLineOffset, transform.position + transform.up * halfSize * transform.localScale.y + debugLineOffset, debugLineDuration);

            if (upInfo && upInfo.collider && upInfo.collider.gameObject)
            {
                Tile targetTile = upInfo.collider.GetComponent<Tile>();
                if (targetTile)
                {
                    LineDrawing.DrawCollider(upInfo.collider, debugLineDuration);

                    if (targetTile.child == null && targetTile.bottomKey != Key.Goal && !isStartTile)
                    {
                        ConnectFromBelow(targetTile);
                    }
                }
            }
        }
    }

    void ConnectFromAbove(Tile newParent)
    {
        if (hasSingleParent)
        {
            if (leftParent != null)
            {
                Debug.LogError("left parent not null");
            }

            Debug.Log("Connecting single parent");
            leftParent = newParent;
            leftParent.child = this;
            newParent.transform.position = transform.position + transform.up * (1.0f * transform.localScale.y + snapMargin);
            AttachLeftLine( transform.up * 1.0f);
        }
        else
        {
            if (leftParent == null)
            {
                Debug.Log("Connecting left parent");
                leftParent = newParent;
                leftParent.child = this;
                newParent.transform.position = transform.position + transform.up * (1.0f * transform.localScale.y + snapMargin) + transform.right * (-0.5f * transform.localScale.x);
                AttachLeftLine( transform.up * 1.0f + transform.right * -0.5f);
            }
            else if (rightParent == null)
            {
                Debug.Log("Connecting right parent");
                rightParent = newParent;
                rightParent.child = this;
                newParent.transform.position = transform.position + transform.up * (1.0f * transform.localScale.y + snapMargin) + transform.right * (0.5f * transform.localScale.x);
                AttachRightLine( transform.up * 1.0f + transform.right * 0.5f);
            }
            else
            {
                Debug.LogError("Neither parent null");
            }
        }
    }

    void ConnectFromBelow(Tile newParent)
    {
        if (hasSingleParent)
        {
            if (leftParent != null)
            {
                Debug.LogError("left parent not null");
            }

            Debug.Log("Connecting single parent");
            leftParent = newParent;
            leftParent.child = this;
            transform.position = newParent.transform.position + transform.up * (-1.0f * transform.localScale.y + -snapMargin);
            AttachLeftLine( transform.up * 1.0f);
        }
        else
        {
            if (leftParent != null || rightParent != null)
            {
                Debug.LogError("Both parents not null on two parent tile being dragged");
            }


            Debug.Log("Connecting left parent");
            leftParent = newParent;
            leftParent.child = this;
            transform.position = newParent.transform.position + transform.up * (-1.0f * transform.localScale.y + -snapMargin) + transform.right * (0.5f * transform.localScale.x);
            AttachLeftLine( transform.up * 1.0f + transform.right * -0.5f);
        }
    }

    void RemoveAllParents()
    {
        if (leftParent)
        {
            leftParent.child = null;
            leftParent = null;
        }
        if (rightParent)
        {
            rightParent.child = null;
            rightParent = null;
        }

        ResetLines();
    }

    void RemoveAParent(Tile oldParent)
    {
        if (leftParent == oldParent)
        {
            leftParent.child = null;
            leftParent = null;
            if (hasSingleParent)
            {
                ResetSingleLine();
            }
            else
            {
                ResetLeftLine();
            }
        }
        else if (rightParent == oldParent)
        {
            rightParent.child = null;
            rightParent = null;
            ResetRightLine();
        }
        else
        {
            Debug.LogError("Could not find parent to remove");
        }
    }

    void MakeGoalTile()
    {
        goalEffect.SetActive(true);
        GetComponent<Renderer>().material = goalTileMaterial;
        Validator.SetGoalTile(this);
        tileType = TileType.Goal;
    }

    void MakeStartTile()
    {
        GetComponent<Renderer>().material = startTileMaterial;
        tileType = TileType.Start;
    }

    void MakeMiddleTile()
    {
        tileType = TileType.Middle;
    }

    public bool IsValid()
    {
        if (isStartTile)
        {
            return true;
        }

        if (hasSingleParent)
        {
            if (leftParent)
            {
                return topKeys[0] == leftParent.bottomKey && leftParent.IsValid();
            }
            else
            {
                return false;
            }
        }
        else
        {
            if (!leftParent || !rightParent)
            {
                return false;
            }
            else
            {
                return topKeys.Contains( leftParent.bottomKey ) && leftParent.IsValid()
                    && topKeys.Contains( rightParent.bottomKey ) && rightParent.IsValid();
            }
        }
    }

    void ResetLines()
    {
        if (topKeys.Count == 1)
        {
            if (!isStartTile)
            {
                ResetSingleLine();
            }
        }
        else
        {
            ResetLeftLine();
            ResetRightLine();
        }
    }

    void ResetSingleLine()
    {
        AttachLeftLine(transform.up * idleLineVerticalDistance);
    }

    void ResetLeftLine()
    {
        AttachLeftLine(transform.up * idleLineVerticalDistance + transform.right * -idleLineHorizontalDistance);
    }

    void ResetRightLine()
    {
        AttachRightLine(transform.up * idleLineVerticalDistance + transform.right * idleLineHorizontalDistance);
    }

    void AttachLeftLine(Vector3 parentPos)
    {
        lineRenderers[0].SetPosition(1, parentPos);
    }

    void AttachRightLine(Vector3 parentPos)
    {
        lineRenderers[1].SetPosition(1, parentPos);
    }
}
