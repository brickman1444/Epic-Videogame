﻿using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

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
    Tile _parent = null;
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
    Key _topKey = Key.Invalid;
    [SerializeField]
    Key _bottomKey = Key.Invalid;
    [SerializeField]
    GameObject goalEffect = null;
    [SerializeField]
    Material startTileMaterial = null;
    [SerializeField]
    Material goalTileMaterial = null;

    LineRenderer lineRenderer = null;

    const float halfSize = 0.51f;

    public bool isStartTile
    {
        get { return tileType == TileType.Start; }
    }

    public Tile parent
    {
        get { return _parent; }
        private set { _parent = value; }
    }

    public Key topKey
    {
        get { return _topKey; }
        private set { _topKey = value; }
    }

    public Key bottomKey
    {
        get { return _bottomKey; }
        private set { _bottomKey = value; }
    }

	// Use this for initialization
	void Start () {
        lineRenderer = GetComponentInChildren<LineRenderer>();
	}

    public void Initialize(string topKeyString, string bottomKeyString, string text)
    {
        topKey = (Key) Enum.Parse(typeof(Key), topKeyString, true);
        bottomKey = (Key)Enum.Parse(typeof(Key), bottomKeyString, true);

        if (bottomKey == Key.Goal)
        {
            MakeGoalTile();
        }

        if (topKey == Key.Start)
        {
            MakeStartTile();
        }

        if (bottomKey == Key.Start)
        {
            Debug.LogError("Bottom key set to Start");
        }

        if (topKey == Key.Goal)
        {
            Debug.LogError("Top key set to Goal");
        }

        Text uiText = GetComponentInChildren<Text>();
        uiText.text = text;
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
            DisconnectFromParent(parent);
        }
        if (child)
        {
            child.DisconnectFromParent(this);
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
                if (targetTile.parent == null && targetTile.topKey != Key.Start && bottomKey != Key.Goal)
                {
                    //Debug.Log(outInfo.collider.gameObject.name);
                    transform.position = downInfo.transform.position + transform.up * (1.0f * transform.localScale.y + snapMargin);
                    targetTile.ConnectToParent(this);
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
                    if (targetTile.child == null && targetTile.bottomKey != Key.Goal && topKey != Key.Start)
                    {
                        //Debug.Log(outInfo.collider.gameObject.name);
                        transform.position = upInfo.transform.position + -transform.up * (1.0f * transform.localScale.y + snapMargin);
                        this.ConnectToParent(targetTile);
                    }
                }
            }
        }
    }

    void ConnectToParent(Tile newParent)
    {
        parent = newParent;
        parent.child = this;
        lineRenderer.SetPosition(1, new Vector3(0,1f,0.3f));
    }

    void DisconnectFromParent(Tile oldParent)
    {
        oldParent.child = null;
        parent = null;
        lineRenderer.SetPosition(1, Vector3.zero);
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
}
