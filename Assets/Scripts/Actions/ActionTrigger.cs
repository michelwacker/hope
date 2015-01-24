using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class ActionTrigger : MonoBehaviour
{
	public enum Moment
	{
		After,
		Before,
		On
	}

	public int trigger;

	public Moment moment;

	public MonoBehaviour[] actions;

//	public string output = string.Empty;

	protected delegate void ActionDelegate();
	protected ActionDelegate actionDelegate;

	protected void Awake()
	{
		if (actions.Length > 0)
		{
			ITriggerAction a;
			foreach (MonoBehaviour action in actions)
			{
				if (action is ITriggerAction)
				{
					a = (ITriggerAction) action;
					actionDelegate += a.Action;
				}
			}
		}
	}

	protected void CheckTiming(int timesCalled)
	{
		Debug.Log ("CheckTiming " + timesCalled);
		switch (moment)
		{
			case Moment.After:
				if (timesCalled > trigger)
					Action();
				break;
			case Moment.Before:
				if (timesCalled < trigger)
					Action();
				break;
			case Moment.On:
				if (timesCalled == trigger)
					Action();
				break;
		}
	}

	private void Action()
	{
		if (actionDelegate != null)
		{
			actionDelegate();
		}
	}
}

public interface ITriggerAction
{
	void Action();
}
