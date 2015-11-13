using UnityEngine;
using System.Collections;

public class LineDrawer : SingletonBehaviour<LineDrawer>
{
    [SerializeField]
    GameObject linePrefab = null;

    [SerializeField]
    float zOffset = 0.0f;

    [SerializeField]
    float XWidth = 0.0f;

    public void DrawLine(Vector3 startPos, Vector3 endPos, Color color, float duration)
    {
        GameObject lineObject = GameObject.Instantiate<GameObject>(linePrefab);

        LineRenderer renderer = lineObject.GetComponent<LineRenderer>();

        renderer.SetPosition(0, startPos + -transform.forward * zOffset);
        renderer.SetPosition(1, endPos + -transform.forward * zOffset);

        renderer.material.color = color;

        GameObject.Destroy(lineObject, duration);
    }

    public void DrawX(Vector3 position, Color color, float duration)
    {
        DrawLine(position + -Vector3.up * XWidth * 0.5f + -Vector3.right * XWidth * 0.5f, position + Vector3.up * XWidth * 0.5f + Vector3.right * XWidth * 0.5f, color, duration); // /
        DrawLine(position + -Vector3.up * XWidth * 0.5f + Vector3.right * XWidth * 0.5f, position + Vector3.up * XWidth * 0.5f + -Vector3.right * XWidth * 0.5f, color, duration);// \
    }
}
