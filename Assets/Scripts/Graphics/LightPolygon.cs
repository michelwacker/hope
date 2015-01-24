using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LightPolygon : MonoBehaviour {

	public GameObject v1;
	public GameObject v2;
	public GameObject v3;
	public GameObject v4;

	public GameObject sun;

	Mesh msh;
	Vector3[] windowVertices;
	List<Vector2> polygonVertices;
	Vector3[] sunWindowVerticesDirections = new Vector3[4];
	Vector3[] sunRays = new Vector3[2];
	int[] sunRaysIndex = new int[2];
	Triangulator tr;
	
	void Start () {
		polygonVertices = new List<Vector2>();
		windowVertices = new Vector3[] {
			v1.transform.position, 
			v2.transform.position,
			v3.transform.position,
			v4.transform.position,
		};
		// Use the triangulator to get indices for creating triangles


		/*msh.vertices = vertices;
		msh.triangles = indices;
		msh.RecalculateNormals();
		msh.RecalculateBounds();*/
		
		// Set up game object with mesh;

		
	}
	
	// Update is called once per frame
	void Update () {
		createRayCasts();
		tr = new Triangulator(polygonVertices.ToArray());
		int[] indices = tr.Triangulate();
		
		// Create the Vector3 vertices
		Vector3[] vertices = new Vector3[polygonVertices.Count];
		for (int i=0; i<vertices.Length; i++) {
			vertices[i] = new Vector3(polygonVertices[i].x, polygonVertices[i].y, 0);
		}
		if(msh == null) {
			msh = new Mesh();
			gameObject.AddComponent(typeof(MeshRenderer));
			MeshFilter filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
			filter.mesh = msh;
		}
		msh.vertices = vertices;
		msh.triangles = indices;
		msh.RecalculateNormals();
		msh.RecalculateBounds();
		
	}

	private void createRayCasts() {
		polygonVertices.Clear();
		Vector3 sunPos = sun.transform.position;

		for(int i = 0; i < windowVertices.Length; i++) {
			Vector3 windowVertex = windowVertices[i];
			sunWindowVerticesDirections[i] = windowVertex - sunPos;
		}

		float maxAngle = -99;
		for(int i = 0; i < sunWindowVerticesDirections.Length; i++) {
			Vector3 dir = sunWindowVerticesDirections[i];
			for(int j = 0; j < sunWindowVerticesDirections.Length; j++) {
				Vector3 dir2 = sunWindowVerticesDirections[j];
				float angle = Vector3.Angle(dir, dir2);
				if(angle > maxAngle) {
					sunRays[0] = dir;
					sunRays[1] = dir2;
					sunRaysIndex[0] = i;
					sunRaysIndex[1] = j;
					maxAngle = angle;
				}
			}
		}

		/*int initialIndex;
		int endIndex;
		if( Mathf.Abs(sunRaysIndex[1] - sunRaysIndex[0]) == 1) {
			initialIndex = sunRaysIndex[1];
			endIndex = sunRaysIndex[0];

		} else {
			initialIndex = sunRaysIndex[0];
			endIndex = sunRaysIndex[1];
		}
		bool finishTurn = false;
		for(int k = initialIndex; k != endIndex;) {
			polygonVertices.Add(windowVertices[k]);
			k++;
			if(k >= windowVertices.Length) {
				finishTurn = true;
				break;
			}
		}*/


		Debug.Log("**********");
		Debug.Log("sunRays A " + sunRaysIndex[0] + " B " + sunRaysIndex[1]);

		if(sunRaysIndex[0] != 0) {
			polygonVertices.Add(windowVertices[0]);
			polygonVertices.Add(windowVertices[sunRaysIndex[1]]);
		} else {
			polygonVertices.Add(windowVertices[0]);
		}

		Wall wall = null;
		if(sunRaysIndex[0] != 0) {
			for(int i = sunRays.Length - 1; i >= 0 ; i--) {
				Vector3 sunRay = sunRays[i];
				RaycastHit hit;
				Physics.Raycast(sunPos, sunRay, out hit);
				if(wall == null) {
					wall = hit.collider.gameObject.GetComponent<Wall>();
					Debug.DrawLine(sunPos, hit.point, Color.red);
				} else {
					Wall wall2 = hit.collider.gameObject.GetComponent<Wall>();
					polygonVertices.Add(wall2.getMiddleCorner(wall));
					Debug.DrawLine(sunPos, hit.point, Color.blue);
				}
				polygonVertices.Add(hit.point);
				
			}
		} else {
			for(int i = 0; i < sunRays.Length ; i++) {
				Vector3 sunRay = sunRays[i];
				RaycastHit hit;
				Physics.Raycast(sunPos, sunRay, out hit);
				if(wall == null) {
					wall = hit.collider.gameObject.GetComponent<Wall>();
					Debug.DrawLine(sunPos, hit.point, Color.red);
				} else {
					Wall wall2 = hit.collider.gameObject.GetComponent<Wall>();
					polygonVertices.Add(wall2.getMiddleCorner(wall));
					Debug.DrawLine(sunPos, hit.point, Color.blue);
				}
				polygonVertices.Add(hit.point);

			}
		}

		if(sunRaysIndex[0] != 0) {
			polygonVertices.Add(windowVertices[sunRaysIndex[0]]);
		} else {
			if(sunRaysIndex[1] != 1) {
				polygonVertices.Add(windowVertices[sunRaysIndex[1]]);
			} 
			polygonVertices.Add(windowVertices[1]);
		}


		/*if(sunRaysIndex[1] > 1) {
			polygonVertices.Add(windowVertices[sunRaysIndex[1]]);
 		}*/
		/*if(finishTurn) {
			Debug.Log("vertices " + polygonVertices.Count + " endIndex " + endIndex + " initial Index " + initialIndex );
			for(int k = 0; k != initialIndex;) {
				polygonVertices.Add(windowVertices[k]);
				k--;
				if(k<0) {
					k=windowVertices.Length - 1;
				}
			}

			Debug.Log("end vertices " + polygonVertices.Count);
		}*/
	}
}
