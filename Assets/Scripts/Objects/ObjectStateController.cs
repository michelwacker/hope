using UnityEngine;
using System.Collections;

public class ObjectStateController : MonoBehaviour, IUseInteraction, IBreakInteraction
{
	private Animator anim;
	void Awake()
	{
		anim = GetComponent<Animator> ();
		if (anim == null)
		{
			Debug.LogError("No animator found in " + name + "! Must be added to set states on interaction.");
		}
	}
	#region IBreakInteraction implementation

	public void Break ()
	{
		if (anim != null)
			anim.SetTrigger ("Break");
	}

	#endregion

	#region IUseInteraction implementation

	public void Use ()
	{
		if (anim != null)
			anim.SetTrigger ("Use");
	}

	#endregion
}
