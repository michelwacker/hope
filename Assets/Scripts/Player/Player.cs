using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
	public float speedFactor = 0.7f;
	public float moveDelay = 0.3f;

	private Transform walkAnchor;
	private Vector3 anchorDist;
	// Use this for initialization
	void Awake ()
	{
		walkAnchor = transform.FindChild ("WalkAnchor");
		if (walkAnchor == null)
		{
			Debug.LogError("WalkAnchor not found in Player!");
		}
		anchorDist = transform.position - walkAnchor.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void WalkToPosition(Vector3 position)
	{
		// if already walking, cancel old tween
		iTween.Stop ();
		// TODO check facing direction

		// TODO set animation
		// calculate movement time based on distance (always move at same speed)
		Vector3 p = position + anchorDist;
		float time = Vector3.Distance (transform.position, p) * speedFactor;

		iTween.MoveTo (gameObject, iTween.Hash (
			"position", p,
			"time", time,
			"delay", moveDelay,
			"easeType", "linear",//"easeInOutQuad",
			"oncomplete", "OnWalkComplete"
		));
	}

	void OnWalkComplete()
	{

		// TODO set animation: stop
	}
}
