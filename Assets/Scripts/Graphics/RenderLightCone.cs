using UnityEngine;
using System.Collections;

public class RenderLightCone : MonoBehaviour {
	public Material mat;
	private float vertexVar;
	public GameObject sun;

	void Update() {
		vertexVar = Mathf.Sin(Time.time ) * 3;
	}

	/*void OnPostRender() {
		//mat.SetPass( 0 );
		GL.Begin( GL.TRIANGLE_STRIP );

		GL.Color( new Color(1,1,1,1.0f) );
		GL.Vertex3( sun.transform.position.x, sun.transform.position.y, sun.transform.position.z );
		GL.Vertex3( 0, vertexVar, 0 );
		GL.Vertex3( vertexVar, vertexVar, 0 );
		GL.End();

		/*GL.Color( new Color(1,1,1,1.0f) );
		GL.Vertex3( v1.transform.position.x, v1.transform.position.y, v1.transform.position.z);
		GL.Vertex3( v2.transform.position.x, v2.transform.position.y, v2.transform.position.z);
		GL.Vertex3( v3.transform.position.x, v3.transform.position.y, v3.transform.position.z);
		GL.Vertex3( v4.transform.position.x, v4.transform.position.y, v4.transform.position.z);
		GL.Vertex3( v5.transform.position.x, v5.transform.position.y, v5.transform.position.z);
		GL.Vertex3( v6.transform.position.x, v6.transform.position.y, v6.transform.position.z);

		GL.End(); 
	} */
}
