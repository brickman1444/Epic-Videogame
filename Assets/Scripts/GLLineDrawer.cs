using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
[RequireComponent(typeof (Camera))]
public class GLLineDrawer : SingletonBehaviour<GLLineDrawer>
{
    [SerializeField]
	Color lineColor = Color.red;
    [SerializeField]
	int lineWidth = 3;

	Material lineMaterial;
	List<Vector2> linePoints = new List<Vector2>();
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
 
	// Sets line endpoints to center of screen and mouse position
	void Update()
	{
        linePoints.Clear();
        linePoints.Add(new Vector2(0.5f, 0.5f));
		linePoints.Add(new Vector2(Input.mousePosition.x/Screen.width, Input.mousePosition.y/Screen.height));
	}
 
	void OnPostRender()
	{
		if (linePoints == null || linePoints.Count < 2)
			return;
 
		float nearClip = cam.nearClipPlane + 0.00001f;
		int end = linePoints.Count - 1;
 
		lineMaterial.SetPass(0);
 
		if (lineWidth == 1)
		{
	        GL.Begin(GL.LINES);
            GL.Color(lineColor);
	        for (int i = 0; i < end; ++i)
			{
	            GL.Vertex(cam.ViewportToWorldPoint(new Vector3(linePoints[i].x, linePoints[i].y, nearClip)));
	            GL.Vertex(cam.ViewportToWorldPoint(new Vector3(linePoints[i+1].x, linePoints[i+1].y, nearClip)));
        	}
    	}
    	else
		{
	        GL.Begin(GL.QUADS);
            GL.Color(lineColor);
            
	        for (int i = 0; i < end; ++i)
			{
                float thisWidth = (float)lineWidth / Screen.width * 0.5f;
	            Vector3 perpendicular = (new Vector3(linePoints[i+1].y, linePoints[i].x, nearClip) -
	                                 new Vector3(linePoints[i].y, linePoints[i+1].x, nearClip)).normalized * thisWidth;
	            Vector3 v1 = new Vector3(linePoints[i].x, linePoints[i].y, nearClip);
	            Vector3 v2 = new Vector3(linePoints[i+1].x, linePoints[i+1].y, nearClip);
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