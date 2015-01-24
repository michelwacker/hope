using UnityEngine;
using System.Collections;

public class UseActionTrigger : ActionTrigger, IUseInteraction
{
	#region IUseInteraction implementation

	public void Use (int timesCalled)
	{
		CheckTiming (timesCalled);
	}

	#endregion
}
