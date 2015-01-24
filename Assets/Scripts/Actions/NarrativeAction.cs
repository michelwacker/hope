using UnityEngine;
using System.Collections;

public class NarrativeAction : MonoBehaviour, ITriggerAction
{
	public string output = string.Empty;

	#region ITriggerAction implementation
	public void Action ()
	{
		Debug.Log ("Action " + output);
		NarrativeController.Write (output);
	}
	#endregion
}
