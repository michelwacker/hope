using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InteractionMenuManager : MonoBehaviour
{
	public Text headline;
	private ObjectInteraction interaction;

	public void HideMenu()
	{
		interaction = null;
		gameObject.SetActive (false);
	}

	public void ShowMenu(GameObject go)
	{
		interaction = go.GetComponent<ObjectInteraction> ();
		if (interaction == null)
		{
			Debug.LogWarning("Target object has no interaction script!");
		}
		if (headline != null)
		{
			headline.text = go.name;
		}
		// if the game object has a menu anchor, use it.
		Transform t = go.transform.FindChild ("MenuAnchor");
		if (t == null)
			t = go.transform;

		Vector3 position = t.position;
		gameObject.transform.position = new Vector3 (position.x, position.y, position.z);
		// show the menu
		gameObject.SetActive (true);

	}

	public void HandleUseDown()
	{
		if (interaction != null)
		{
			interaction.Use();
		}
		HideMenu ();
	}

	public void HandleBreakDown()
	{
		if (interaction != null)
		{
			interaction.Break();
		}
		HideMenu ();
	}

	public void HandleCancelDown()
	{
		HideMenu ();
	}
}
