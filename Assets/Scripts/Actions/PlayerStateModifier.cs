using UnityEngine;
using System.Collections;

public class PlayerStateModifier : MonoBehaviour, ITriggerAction
{
	public Animator playerAnimator;
	public string state;

	void Awake()
	{
		if (playerAnimator == null)
		{
			Debug.LogError("Player animator not set in PlayerStateModifier in " + name);
		}
	}
	#region ITriggerAction implementation

	public void Action ()
	{
		if (playerAnimator != null)
		{
			playerAnimator.SetBool(state, true);
		}
	}

	#endregion



}
