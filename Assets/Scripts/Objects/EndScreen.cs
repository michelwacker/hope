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
			Color c = sprite.color;
			c.a = 1;
			sprite.color = c;
		}
	
	}
}
