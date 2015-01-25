using UnityEngine;
using System.Collections;

public class Sun : MonoBehaviour {
	const float MID_DAY_BEGIN = 0.2f;
	const float MID_DAY_END = 0.45f;

	public GameObject beginPosition;
	public GameObject firstWindowMark;
	public GameObject secondWindowMark;
	public GameObject endPosition;


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		float dayRatio = DayNightController.getDayRatio();
		if(DayNightController.isDay()) {
			if(dayRatio < MID_DAY_BEGIN) {
				float ratio = MathHelper.Map(dayRatio, 0, MID_DAY_BEGIN, 0, 1);
				gameObject.transform.position = Vector3.Lerp(beginPosition.transform.position, firstWindowMark.transform.position, ratio);
			} else if(dayRatio > MID_DAY_BEGIN && dayRatio < MID_DAY_END) {
				float ratio = MathHelper.Map(dayRatio, MID_DAY_BEGIN, MID_DAY_END, 0, 1);
				gameObject.transform.position = Vector3.Lerp(firstWindowMark.transform.position, secondWindowMark.transform.position, ratio);
			} else {
				float ratio = MathHelper.Map(dayRatio, MID_DAY_END, 1, 0, 1);
				gameObject.transform.position = Vector3.Lerp(secondWindowMark.transform.position, endPosition.transform.position, ratio);
			}
		} else {
			gameObject.transform.position = Vector3.Lerp(endPosition.transform.position, beginPosition.transform.position, dayRatio);
		}
		
	}
}
