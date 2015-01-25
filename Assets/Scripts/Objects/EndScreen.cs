using UnityEngine;
using System.Collections;

public class EndScreen : MonoBehaviour {
	// Use this for initialization

	public void Awake()
	{
		if (gameObject.collider2D != null)
			gameObject.collider2D.enabled = false;
		
		Color c = gameObject.transform.renderer.material.color;
		gameObject.transform.renderer.material.color = new Color (c.r,c.g,c.b, 0f);
	}

	void Start () {
		//sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(SceneManager.isGameOver()) {
			iTween.FadeTo (gameObject, iTween.Hash (
				"alpha", 0f,
				"amount", 1f,
				"time", 5f,
				"easetype", "easeInOutSine",
				"looptype", "pingPong"
				));
		}
	
	}
}
