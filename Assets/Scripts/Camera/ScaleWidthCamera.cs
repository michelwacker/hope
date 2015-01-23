using UnityEngine;
using System;

[ExecuteInEditMode]
public class ScaleWidthCamera : MonoBehaviour
{
	public static event Action CAMERA_RESCALE;
	
	public int targetWidth = 640;
	public float pixelsToUnits = 100f;
	
	void Awake ()
	{
		ScaleCamera ();
	}
	
	//	#if UNITY_EDITOR
	//	void Update()
	//	{
	//		ScaleCamera ();
	//	}
	//	#endif
	
	void ScaleCamera()
	{
		int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);
		// calculate new orthographic size
		camera.orthographicSize = height / pixelsToUnits / 2f;
		// send rescale event
		Action handler = CAMERA_RESCALE;
		if (handler != null) handler();
	}
}
