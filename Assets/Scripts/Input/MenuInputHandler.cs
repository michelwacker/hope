using UnityEngine;
using System.Collections;

public class MenuInputHandler : MonoBehaviour {

	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			Application.LoadLevel("Scene1");
		}
	}
}
