using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 pos = transform.position;
		pos.x += Time.deltaTime ;
		transform.position = pos;
		
	}
}
