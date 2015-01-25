using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {

	public GameObject beginPosition;
	public GameObject endPosition;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(DayNightController.isDay()) {
			gameObject.transform.position = Vector3.Lerp(beginPosition.transform.position, endPosition.transform.position, DayNightController.getDayRatio());
		} else {
			gameObject.transform.position = Vector3.Lerp(endPosition.transform.position, beginPosition.transform.position, DayNightController.getDayRatio());
		}
		
	}
}
