using UnityEngine;
using System.Collections;

public class CameraFade : MonoBehaviour {
	
//	public Animator anim;

	public float amount = 1f;
	public float time = 1f;
	
	void Awake () {
//		if(anim == null)
//			Debug.Log("No animator found in CameraFade");

	}
	
	// Use this for initialization
	void Start () {
		iTween.CameraFadeAdd ();
//		FadeOutCamera ();
	}
	
	public void FadeOutCamera()
	{
		iTween.CameraFadeTo (iTween.Hash ("amount", amount, "time", time, "oncomplete", "OnFadeOutComplete", "oncompletetarget", gameObject));
		
//		anim.SetTrigger ("MenuFadeOut");
	}
	public void FadeInCamera()
	{
		iTween.CameraFadeTo (iTween.Hash ("amount", 0f, "time", time, "oncomplete", "OnFadeInComplete", "oncompletetarget", gameObject));
		
		//		anim.SetTrigger ("MenuFadeOut");
	}
	
	private void OnFadeOutComplete()
	{
//		Debug.Log("FadeComplete");
//		FadeInCamera ();
	}
	private void OnFadeInComplete()
	{
		//		Debug.Log("FadeComplete");
//		FadeOutCamera ();
	}
}
