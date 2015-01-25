using UnityEngine;
using System.Collections;

public class MouseMover : MonoBehaviour, ITriggerAction
{
	public Transform foodAnchor;

	private Vector3 initPos;
	private Animator anim;

	void Awake()
	{
		Vector3 p = transform.position;

		initPos = new Vector3 (p.x, p.y, p.z);

		anim = GetComponent<Animator> ();
	}

	#region ITriggerAction implementation
	public void Action ()
	{
		StartCoroutine (WaitForRun());
	}
	#endregion
	IEnumerator WaitForRun()
	{
		yield return new WaitForSeconds (2f);
		Run ();
	}
	void Run()
	{
		anim.SetBool("Feed", false);
		
		gameObject.transform.position = new Vector3 (initPos.x, initPos.y, initPos.z);
		gameObject.SetActive (true);
		
		iTween.MoveTo(gameObject, iTween.Hash(
			"position", foodAnchor.transform.position,
			"onComplete", "OnRunComplete",
			"easeType", "linear"));
	}
	void OnRunComplete()
	{
		if (anim != null)
		{
			anim.SetBool("Feed", true);
		}
	}
}
