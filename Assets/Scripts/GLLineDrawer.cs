using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
[RequireComponent(typeof (Camera))]
public class GLLineDrawer : SingletonBehaviour<GLLineDrawer>
{
    struct Line
    {
        public Vector2 start;
        public Vector2 end;
    }

    [SerializeField]
	Color lineColor = Color.red;
    [SerializeField]
	int lineWidth = 3;

	Material lineMaterial;
	List<Line> lines = new List<Line>();
	Camera cam;
 
	void Start()
	{
        // Unity has a built-in shader that is useful for drawing
        // simple colored things.
        Shader shader = Shader.Find("Hidden/Internal-Colored");
        lineMaterial = new Material(shader);
        lineMaterial.hideFlags = HideFlags.HideAndDontSave;
        // Turn on alpha blending
        lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        // Turn backface culling off
        lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        // Turn off depth writes
        lineMaterial.SetInt("_ZWrite", 0);
		cam = GetComponent<Camera>();
	}

    public void DrawLine(Vector3 startPos, Vector3 endPos, float duration)
    {
        Line line;
        line.start = cam.WorldToViewportPoint(startPos);
        line.end = cam.WorldToViewportPoint(endPos);
        lines.Add(line);
    }
 
	void OnPostRender()
	{
		if (lines == null || lines.Count < 2)
			return;
 
		lineMaterial.SetPass(0);

        float nearClip = cam.nearClipPlane + 0.00001f;
        if (lineWidth == 1)
        {
            GL.Begin(GL.LINES);
            GL.Color(lineColor);
            foreach (Line line in lines)
            {
                GL.Vertex(cam.ViewportToWorldPoint(new Vector3(line.start.x, line.start.y, nearClip)));
                GL.Vertex(cam.ViewportToWorldPoint(new Vector3(line.end.x, line.end.y, nearClip)));
            }
        }
        else
        {
            GL.Begin(GL.QUADS);
            GL.Color(lineColor);

            float thisWidth = (float)lineWidth / Screen.width * 0.5f;

            foreach (Line line in lines)
            {
                Vector3 perpendicular = (new Vector3(line.end.y, line.start.x, nearClip) -
                                        new Vector3(line.start.y, line.end.x, nearClip)).normalized * thisWidth;
                Vector3 v1 = new Vector3(line.start.x, line.start.y, nearClip);
                Vector3 v2 = new Vector3(line.end.x, line.end.y, nearClip);
                GL.Vertex(cam.ViewportToWorldPoint(v1 - perpendicular));
                GL.Vertex(cam.ViewportToWorldPoint(v1 + perpendicular));
                GL.Vertex(cam.ViewportToWorldPoint(v2 + perpendicular));
                GL.Vertex(cam.ViewportToWorldPoint(v2 - perpendicular));
            }
        }
        GL.End();
    
	}

	void OnApplicationQuit()
	{
		DestroyImmediate(lineMaterial);
	}
}