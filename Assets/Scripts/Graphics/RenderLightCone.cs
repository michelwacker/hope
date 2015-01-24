using UnityEngine;
using System.Collections;

public class RenderLightCone : MonoBehaviour {
	public Material mat;
	private float vertexVar;

	void Update() {
		vertexVar = Mathf.Sin(Time.time ) * 3;
	}

	void OnPostRender() {
		//mat.SetPass( 0 );
		GL.Begin( GL.TRIANGLES );

		GL.Color( new Color(1,1,1,1.0f) );
		GL.Vertex3( vertexVar, 0, 0 );
		GL.Vertex3( 0, vertexVar, 0 );
		GL.Vertex3( vertexVar, vertexVar, 0 );

		GL.End();
	}
}
