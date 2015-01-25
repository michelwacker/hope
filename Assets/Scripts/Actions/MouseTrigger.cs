using UnityEngine;
using System.Collections;

public class MouseTrigger : MonoBehaviour, ITriggerAction
{
	
	public GameObject mouse;

	#region ITriggerAction implementation
	public void Action ()
	{
		if (mouse != null)
		{
			MouseMover m = mouse.GetComponent<MouseMover>();
			if (m != null)
			{
				m.Action();
			}
		}
	}
	#endregion
}
