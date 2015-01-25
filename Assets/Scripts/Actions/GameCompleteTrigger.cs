using UnityEngine;
using System.Collections;

public class GameCompleteTrigger : MonoBehaviour, ITriggerAction
{
	public int madness = 0;
	#region ITriggerAction implementation
	public void Action ()
	{
		Debug.Log ("GAME COMPLETE!");
		Player.SetMadness (madness);

		SceneManager.GameComplete();
	}
	#endregion
	
}
