using UnityEngine;
using System.Collections;

public class InputCheck : MonoBehaviour
{
	public bool allowMultitouch;
	public bool blockAfterSelection;
	public bool allowDrag;
	public MonoBehaviour inputHandler;
	private IInputHandler _inputHandler;


	private bool blocked;
	private bool isDragging;
	
	void Awake()
	{
		if (inputHandler != null && inputHandler is IInputHandler)
		{
			_inputHandler = (IInputHandler) inputHandler;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (blocked) return;
		
		#if UNITY_EDITOR
		
		//		Debug.Log("Unity Editor");
		if (Input.GetMouseButtonDown(0))
		{
			RayCastToPosition(Input.mousePosition);
			if (allowDrag) isDragging = true;
		}
		
		if (Input.GetMouseButtonUp(0))
		{
			//			Debug.Log("Mouse Button Up");
			isDragging = false;
			StopInput();
		}
		
		if (isDragging)
		{
			UpdatePosition(Input.mousePosition);
		}
		
		
		#elif UNITY_IPHONE || UNITY_ANDROID
		
		if (allowMultitouch)
		{
			foreach (Touch touch in Input.touches)
			{
				EvalTouch(touch);
			}
		}
		else if (Input.touches.Length > 0)
		{
			EvalTouch(Input.touches[0]);
		}
		#endif
	}
	
	protected void RayCastToPosition(Vector2 position, int fingerId = -1)
	{
		RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(position),Vector2.zero);
		
		if (hit.collider != null)
		{
			Debug.Log ("You've touched " + hit.collider.name);
			if (_inputHandler != null)
			{
				_inputHandler.HandleInputDown(hit.collider, fingerId);
				
				if (blockAfterSelection) blocked = true;
			}
		}
	}
	
	protected void UpdatePosition(Vector2 position, int fingerId = -1)
	{
		if (_inputHandler != null )
		{
			_inputHandler.HandleInputUpdate(position, fingerId);
		}
	}
	
	protected void StopInput(int fingerId = -1)
	{
		if (_inputHandler != null)
		{
			_inputHandler.HandleInputUp(fingerId);
		}
	}
	
	protected void EvalTouch(Touch touch)
	{
		if (touch.phase == TouchPhase.Began)
		{
			RayCastToPosition(touch.position, touch.fingerId);
		}
		else if (touch.phase == TouchPhase.Moved)
		{
			UpdatePosition(touch.position, touch.fingerId);
		}
		else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
		{
			StopInput(touch.fingerId);
		}
	}
}

public interface IInputHandler
{
	void HandleInputDown(Collider2D target, int fingerId = -1);
	void HandleInputUpdate(Vector2 position, int fingerId = -1);
	void HandleInputUp(int fingerId = -1);
}