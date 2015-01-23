using UnityEngine;
using System.Collections;

public class CameraFade : MonoBehaviour {
	
	public Animator anim;
	
	
	void Awake () {
		if(anim == null)
			Debug.Log("No animator found in CameraFade");
	}
	
	// Use this for initialization
	void Start () {
		iTween.CameraFadeAdd ();
	}
	
	public void FadeOutCamera()
	{
		iTween.CameraFadeTo (iTween.Hash ("amount", 1f, "time", 1f, "oncomplete", "OnFadeComplete", "oncompletetarget", gameObject));
		
		anim.SetTrigger ("MenuFadeOut");
	}
	
	private void OnFadeComplete()
	{
		Debug.Log("FadeComplete");
	}
}
