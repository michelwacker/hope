using UnityEngine;
using System.Collections;

public class CrazinessModifier : MonoBehaviour, IUseInteraction, IBreakInteraction
{
	[Range(-3, 3)]
	public int crazinessOnBreak = 0;
	[Range(-3, 3)]
	public int crazinessOnUse = 0;

	#region IBreakInteraction implementation
	public void Break ()
	{
		Debug.Log ("Break!");
		// TODO add player craziness
	}
	#endregion

	#region IUseInteraction implementation
	public void Use ()
	{
		Debug.Log ("Use!");
		// TODO add player craziness
	}
	#endregion
}
