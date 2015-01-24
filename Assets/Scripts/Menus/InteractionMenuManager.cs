using UnityEngine;
using System.Collections;

public class InteractionMenuManager : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		
	}

	public void HideMenu()
	{
		gameObject.SetActive (false);
	}

	public void ShowMenu(Vector3 position)
	{
		gameObject.transform.position = new Vector3 (position.x, position.y, position.z);

		gameObject.SetActive (true);
	}
}
