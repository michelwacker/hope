using UnityEngine;
using System.Collections;

public class NarrativeUseTrigger : MonoBehaviour, IUseInteraction
{
	public enum Moment
	{
		After,
		Before,
		On
	}

	public int trigger;

	public Moment moment;

	public string output = string.Empty;

	#region IUseInteraction implementation

	public void Use (int timesCalled)
	{
		switch (moment)
		{
			case Moment.After:
				if (trigger > timesCalled)
					Action();
				break;
			case Moment.Before:
				if (trigger < timesCalled)
					Action();
				break;
			case Moment.On:
				if (trigger == timesCalled)
					Action();
				break;
		}
	}

	#endregion

	private void Action()
	{
		NarrativeController.Write (output);
	}
}
