using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour
{

	public InteractionMenuManager interactionMenuManager;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

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
		HideInteractionMenu();
	}
	void HandleWalkComplete (GameObject gameObject)
	{
		Debug.Log ("HandleWalkComplete");
		ShowInteractionMenu(gameObject.transform.position);
	}

	public void HandleUseDown()
	{
		Debug.Log ("HandleUseDown");
		HideInteractionMenu ();

	}
	public void HandleBreakDown()
	{
		Debug.Log ("HandleBreakDown");
		HideInteractionMenu ();
	}

	private void ShowInteractionMenu(Vector3 position)
	{
		interactionMenuManager.ShowMenu (position);
	}

	private void HideInteractionMenu()
	{
		interactionMenuManager.HideMenu ();
	}
}
