using UnityEngine;
using System.Collections;

public static class LineDrawing
{
    public static void DrawLine( Vector3 startPos, Vector3 endPos, Color color, float duration )
    {
        Debug.DrawLine(startPos, endPos, Color.red, duration);

        GLLineDrawer.instance.DrawLine(startPos, endPos, color, duration);
    }

    public static void DrawCollider(Collider2D collider, Color color, float duration )
    {
        BoxCollider2D boxCollider = (BoxCollider2D)collider;

        Vector3 size = boxCollider.bounds.size;
        Vector3 lowerLeft = boxCollider.bounds.min;
        Vector3 upperLeft = lowerLeft + size.SetX(0);
        Vector3 upperRight = boxCollider.bounds.max;
        Vector3 lowerRight = lowerLeft + size.SetY(0);

        DrawLine(lowerLeft,  upperLeft,  color, duration);
        DrawLine(upperLeft,  upperRight, color, duration);
        DrawLine(upperRight, lowerRight, color, duration);
        DrawLine(lowerRight, lowerLeft,  color, duration);
    }

    public static void DrawX(Vector3 position, Color color, float duration)
    {
        GLLineDrawer.instance.DrawX(position, color, duration);
    }
}
