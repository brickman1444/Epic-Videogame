using UnityEngine;
using System.Collections;

public static class LineDrawing
{
    public static void DrawLine( Vector3 startPos, Vector3 endPos, float duration )
    {
        Debug.DrawLine(startPos, endPos, Color.red, duration);

        GLLineDrawer.instance.DrawLine(startPos, endPos, duration);
    }

    public static void DrawCollider(Collider2D collider, float duration )
    {
        BoxCollider2D boxCollider = (BoxCollider2D)collider;

        Vector3 size = boxCollider.bounds.size;
        Vector3 lowerLeft = boxCollider.bounds.min;
        Vector3 upperLeft = lowerLeft + size.SetX(0);
        Vector3 upperRight = boxCollider.bounds.max;
        Vector3 lowerRight = lowerLeft + size.SetY(0);

        DrawLine(lowerLeft, upperLeft, duration);
        DrawLine(upperLeft, upperRight, duration);
        DrawLine(upperRight, lowerRight, duration);
        DrawLine(lowerRight, lowerLeft, duration);
    }
}
