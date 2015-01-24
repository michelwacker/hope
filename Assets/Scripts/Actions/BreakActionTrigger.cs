using UnityEngine;
using System.Collections;

public class BreakActionTrigger : ActionTrigger, IBreakInteraction
{
	#region IBreakInteraction implementation

	public void Break (int timesCalled)
	{
		CheckTiming (timesCalled);
	}

	#endregion
}
