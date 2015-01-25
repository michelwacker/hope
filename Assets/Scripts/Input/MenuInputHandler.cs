using UnityEngine;
using System.Collections;

public class MenuInputHandler : MonoBehaviour {

	#region IInputHandler implementation
	public void HandleInputDown (Collider2D target, int fingerId = -1)
	{
		Debug.Log("menu!!!");
		Application.LoadLevel ("Scene1");
	}
	public void HandleInputUpdate (Vector2 position, int fingerId = -1)
	{
		// empty
	}
	public void HandleInputUp (int fingerId = -1)
	{
		// empty
	}
	#endregion
}
