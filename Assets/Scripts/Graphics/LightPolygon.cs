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
	}
	MeshFilter filter;
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
		Mesh msh = new Mesh();
		if(filter == null) {
			gameObject.AddComponent(typeof(MeshRenderer));
			filter = gameObject.AddComponent(typeof(MeshFilter)) as MeshFilter;
		}
		filter.mesh = msh;
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
				if(dir.Equals(dir2)) {
					continue;
				}
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

		for(int i=0; i <= sunRaysIndex[0]; i++) {
			polygonVertices.Add(windowVertices[i]);
		}


		Wall wall = null;

		for(int i = 0; i < sunRays.Length ; i++) {
			Vector3 sunRay = sunRays[i];
			RaycastHit hit;
			Physics.Raycast(sunPos, sunRay, out hit);
			if(wall == null) {
				wall = hit.collider.gameObject.GetComponent<Wall>();
				Debug.DrawLine(sunPos, hit.point, Color.red);
			} else {

				Wall wall2 = hit.collider.gameObject.GetComponent<Wall>();
				if(!wall2.Equals(wall)) {
					polygonVertices.Add(wall2.getMiddleCorner(wall));
					Debug.DrawLine(sunPos, hit.point, Color.blue);
				}
			}
			polygonVertices.Add(hit.point);
		}

		for(int i = sunRaysIndex[1]; i<=3; i++) {
			polygonVertices.Add(windowVertices[i]);
		}

	}
}
