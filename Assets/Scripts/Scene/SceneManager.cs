using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{

	public InteractionMenuManager interactionMenuManager;

	void OnEnable()
	{
		Player.WalkBegin += HandleWalkBegin;
		Player.WalkComplete += HandleWalkComplete;
	}

	void OnDisable()
	{
		Player.WalkBegin -= HandleWalkBegin;
		Player.WalkComplete -= HandleWalkComplete;
	}
	void HandleWalkBegin (GameObject gameObject)
	{
		Debug.Log ("HandleWalkBegin");
		interactionMenuManager.HideMenu ();
	}
	void HandleWalkComplete (GameObject gameObject)
	{
		Debug.Log ("HandleWalkComplete");
		interactionMenuManager.ShowMenu (gameObject);
	}
}
