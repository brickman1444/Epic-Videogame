using UnityEngine;
using System.Collections;

public class LineDrawer : SingletonBehaviour<LineDrawer>
{
    [SerializeField]
    GameObject linePrefab = null;

    public void DrawLine(Vector3 startPos, Vector3 endPos, float duration)
    {
        GameObject lineObject = GameObject.Instantiate<GameObject>(linePrefab);

        LineRenderer renderer = lineObject.GetComponent<LineRenderer>();

        renderer.SetPosition(0, startPos );
        renderer.SetPosition(1, endPos );

        GameObject.Destroy(lineObject, duration);
    }
}
