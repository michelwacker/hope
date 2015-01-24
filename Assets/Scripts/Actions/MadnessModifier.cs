using UnityEngine;
using System.Collections;

public class MadnessModifier : MonoBehaviour, ITriggerAction
{
	[Range(-3, 3)]
	public int madness = 0;
	#region ITriggerAction implementation
	public void Action ()
	{
		Player.AddMadness (madness);
	}
	#endregion
}
