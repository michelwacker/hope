using UnityEngine;
using System.Collections;

public class Friend : MonoBehaviour, ITriggerAction
{

	public bool isVisible;

	public void Awake()
	{
		if (gameObject.collider2D != null)
			gameObject.collider2D.enabled = false;

		Color c = gameObject.transform.renderer.material.color;
		gameObject.transform.renderer.material.color = new Color (c.r,c.g,c.b, 0f);
	}
	public void Show()
	{
		Action ();
	}
//	public void Hide()
//	{
//		iTween.FadeTo (gameObject, iTween.Hash (
//			"alpha", 0f,
//			"time", 0.5f,
//			"easetype", "easeInOutSine",
//			"oncomplete", "OnHideComplete"
//			));
//	}
	#region ITriggerAction implementation

	public void Action ()
	{
//		gameObject.SetActive (true);

		if (gameObject.collider2D != null)
			gameObject.collider2D.enabled = true;

		iTween.MoveTo (gameObject, iTween.Hash (
			"y", transform.position.y + 0.4f,
			"time", 2f,
			"easetype", "easeInOutSine",
			"looptype", "pingPong"
			));

		iTween.FadeFrom (gameObject, iTween.Hash (
			"alpha", 1f,
			"amount", 0f,
			"time", 3f,
			"easetype", "easeInOutSine",
			"looptype", "pingPong"
			));

		isVisible = true;
	}

	#endregion

//	void OnHideComplete()
//	{
//		if (gameObject.collider2D != null)
//			gameObject.collider2D.enabled = false;
//		iTween.Stop ();
//
//		gameObject.SetActive (false);
//	}
}
