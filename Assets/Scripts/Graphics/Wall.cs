using UnityEngine;
using System.Collections;

public class Wall : MonoBehaviour {

	public GameObject corner1;
	public GameObject corner2;
	public GameObject[] corners = new GameObject[2];
	// Use this for initialization
	void Start () {
		corners[0] = corner1;
		corners[1] = corner2;
	}

	public Vector3 getMiddleCorner(Wall wall) {
		Vector3 cornerVertex = Vector3.zero;
		foreach(GameObject corner in wall.corners) {
			if(corner.Equals(corner1) || corner.Equals(corner2)) {
				cornerVertex = corner.transform.position;
				break;
			}
		}
		return cornerVertex;
	}
}
