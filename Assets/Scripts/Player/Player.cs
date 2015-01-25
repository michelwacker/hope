using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
	public static event Action<GameObject> WalkBegin;
	public static event Action<GameObject> WalkComplete;
	
	public static void AddMadness(int madness)
	{
		instance.madness += madness;
		Debug.Log("Player madness is " + instance.madness);
	}
	public static int currentMadness
	{
		get
		{
			return instance.madness;
		}
	}

	private static Player instance;

	public float speedFactor = 0.5f;
	public float moveDelay = 0.2f;

	public float verticalScaleFactor = 0.2f;

	public AudioClip walkingSound;

	public GameObject book;

	private AudioSource source;
	private float initY;
	private GameObject currentTarget;

	private int madness = 0;

	public bool facingRight
	{
		get
		{
			return transform.localScale.x > 0f;
		}
	}

	private Transform walkAnchor;
	private Vector3 anchorDist;

	private Animator anim;
	
	// Use this for initialization
	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
		}

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

		anim = GetComponent<Animator> ();

		initY = transform.position.y;
	}

//	public void Read()
//	{
//		SetState ("Read", true);
//	}
//	public void Look()
//	{
//		SetState ("Look", true);
//	}
//	public void Crouch()
//	{
//		SetState ("Crouch", true);
//	}
//	public void ShowBack()
//	{
//		SetState ("ShowBack", true);
//	}
	public void Draw()
	{
		SetState ("Draw", true);

		StartCoroutine (WaitDrawing());
	}

	private IEnumerator WaitDrawing()
	{
		yield return new WaitForSeconds (2f);

		SetState ("Draw", false);
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
		bool movingRight = (p.x >= transform.position.x);
		if ((movingRight && !facingRight) || (!movingRight && facingRight))
		{
			Flip();
		}
		
		SetState ("Read", false);
		SetState ("Sleep", false);
		SetState ("Look", false);
		SetState ("Draw", false);
		SetState ("Crouch", false);
		SetState ("ShowBack", false);
		if (book != null)
		{
			book.SetActive(true);
		}

		if (Vector3.Distance(p, transform.position) > 0.05)
		{
			StartCoroutine(StartWalking (p, moveDelay));
		}
		else
		{
			OnWalkComplete();
		}
	}

	private IEnumerator StartWalking(Vector3 p, float delay)
	{
		if (delay > 0f)
			yield return new WaitForSeconds (delay);

		SetState ("Walk", true);
		
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
		StopCoroutine (WaitDrawing());
		iTween.Stop ();
		
		SetState ("Walk", false);

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

	void SetTrigger(string state)
	{
		if (anim != null)
		{
			anim.SetTrigger(state);
		}
	}
	void SetState(string state, bool mode)
	{
		if (anim != null)
		{
			anim.SetBool(state, mode);
		}
	}
}
