using UnityEngine;
using System.Collections;

public class MathHelper  {

	public static float Map (float value, float fromMin, float fromMax, float toMin, float toMax) 
	{
		return toMin + (toMax - toMin) * ((value - fromMin) / (fromMax - fromMin));
	}
}
