using UnityEngine;
using System.Collections;

public class EndScreen : MonoBehaviour {
	SpriteRenderer sprite;
	// Use this for initialization
	void Start () {
		sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if(SceneManager.isGameOver()) {
			iTween.FadeFrom (gameObject, iTween.Hash (
				"alpha", 1f,
				"amount", 0f,
				"time", 3f,
				"easetype", "easeInOutSine",
				"looptype", "pingPong"
				));
		}
	
	}
}
