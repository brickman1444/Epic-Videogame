using UnityEngine;
using System.Collections;

public static class LineDrawing
{
    public static void DrawLine( Vector3 startPos, Vector3 endPos, float duration )
    {
        Debug.DrawLine(startPos, endPos, Color.red, duration);

        GLLineDrawer.instance.DrawLine(startPos, endPos, duration);
    }
}
