using UnityEngine;
using System.Collections;

public class SceneInputHandler : MonoBehaviour, IInputHandler
{	
	public Player player;
	// Use this for initialization
	void Start ()
	{
		
	}

	#region IInputHandler implementation
	public void HandleInputDown (Collider2D target, int fingerId = -1)
	{
		if (player != null)
		{
			Transform t = target.transform.FindChild("WalkAnchor");
			if (t == null)
				t = target.transform;
			player.WalkToPosition(t.position);
		}
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
