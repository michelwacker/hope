using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
	public static event Action<GameObject> WalkBegin;
	public static event Action<GameObject> WalkComplete;

	public float speedFactor = 0.5f;
	public float moveDelay = 0.2f;

	public float verticalScaleFactor = 0.2f;

	public AudioClip walkingSound;

	private AudioSource source;
	private float initY;
	private GameObject currentTarget;

	public bool facingRight
	{
		get
		{
			return transform.localScale.x > 0f;
		}
	}

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

		source = GetComponent<AudioSource> ();
		if (source == null)
		{
			Debug.LogError("No audio source in player found!");
		}

		initY = transform.position.y;
	}

	void Start()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

	public void WalkToPosition(Vector3 position, GameObject gameObject)
	{
		// if already walking, cancel old tween
		StopWalking ();
		// start new walk
		currentTarget = gameObject;
		// dispatch event
		Action<GameObject> handler = WalkBegin;
		if (handler != null) handler(gameObject);
		// calculate anchor dependant position
		Vector3 p = position + anchorDist;
		// synch movement directoin
		bool movingRight = (p.x > transform.position.x);
		if ((movingRight && !facingRight) || (!movingRight && facingRight))
		{
			Flip();
		}

		StartCoroutine(StartWalking (p, moveDelay));
	}

	private IEnumerator StartWalking(Vector3 p, float delay)
	{
		if (delay > 0f)
			yield return new WaitForSeconds (delay);

		// TODO set animation
		
		if (source != null)
		{
			source.clip = walkingSound;
			source.Play();
		}
		
		// calculate movement time based on distance (always move at same speed)
		float time = Vector3.Distance (transform.position, p) * speedFactor;
		
		iTween.MoveTo (gameObject, iTween.Hash (
			"position", p,
			"time", time,
			"easeType", "linear",//"easeInOutQuad",,
			"onupdate", "OnWalkUpdate",
			"oncomplete", "OnWalkComplete"
			));
	}

	private void StopWalking()
	{
		iTween.Stop ();
		// TODO set animation
		if (source != null)
		{
			source.Stop ();
		}
		currentTarget = null;
	}

	void OnWalkUpdate()
	{
		// scale player depending on y position
		Vector3 theScale = transform.localScale;
		theScale.y = (1f - verticalScaleFactor) + verticalScaleFactor * transform.position.y / initY;

		float x = Mathf.Abs (theScale.y);
		x *= (theScale.x < 0f) ? -1f : 1f;

		theScale.x = x;
		transform.localScale = theScale;
	}

	void OnWalkComplete()
	{
		Action<GameObject> handler = WalkComplete;
		if (handler != null) handler(currentTarget);

		StopWalking ();
	}

	void Flip()
	{
		// invert graphics scale
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
