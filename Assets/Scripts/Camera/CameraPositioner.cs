using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraPositioner : MonoBehaviour
{
	//	public static float UNITS_PER_PIXEL = 1f / 100f;
	
	public static Vector2 BOUNDS_SCENE_MIN = Vector2.zero;
	public static Vector2 BOUNDS_SCENE_MAX = Vector2.zero;
	public static Vector2 SCENE_SIZE = Vector2.zero;
	
	//	public TestDevice device = TestDevice.iPad;
	public GameObject background;
	public GameObject backgroundDelimiter;
	
	//	public enum TestDevice
	//	{
	//		iPad,
	//		Android
	//	}
	
	void OnEnable()
	{
		ScaleWidthCamera.CAMERA_RESCALE += HandleCameraRescale;
	}
	void OnDisable()
	{
		ScaleWidthCamera.CAMERA_RESCALE -= HandleCameraRescale;
	}
	
	void HandleCameraRescale()
	{
		Camera cam = GetComponent<Camera>();
		
		if (cam != null)
		{
			
			// center camera on background if one has been passed (should be the case)
			if (background != null)
			{
				Bounds bounds = background.renderer.bounds;
				cam.transform.position = new Vector3 (background.transform.position.x, bounds.max.y - cam.orthographicSize, cam.transform.position.z);
			}
			else
			{
				Debug.LogWarning("Failed to adjust camera to missing background!");
			}
			
			float camHalfHeight = cam.orthographicSize;
			float camHalfWidth = cam.aspect * camHalfHeight; 
			
			BOUNDS_SCENE_MAX = new Vector2(cam.transform.position.x + camHalfWidth, cam.transform.position.y + camHalfHeight);
			
			BOUNDS_SCENE_MIN = new Vector2(cam.transform.position.x - camHalfWidth, cam.transform.position.y - camHalfHeight);
			
			if(backgroundDelimiter != null)
			{
				BOUNDS_SCENE_MIN.y = backgroundDelimiter.transform.position.y;
			}
			
			SCENE_SIZE = new Vector2(BOUNDS_SCENE_MAX.x - BOUNDS_SCENE_MIN.x, BOUNDS_SCENE_MAX.y - BOUNDS_SCENE_MIN.y);
			
		}
	}
}
