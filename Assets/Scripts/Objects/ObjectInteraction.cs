using UnityEngine;
using System.Collections;

public class ObjectInteraction : MonoBehaviour
{
	public MonoBehaviour[] interactions;

	protected delegate void UseDelegate (int timesCalled);
	protected UseDelegate useDelegate;

	protected delegate void BreakDelegate (int timesCalled);
	protected BreakDelegate breakDelegate;

	private int timesCalledUse;
	private int timesCalledBreak;

	protected void Awake()
	{
		if (interactions.Length == 0)
		{
			useDelegate = DefaultUse;
			breakDelegate = DefaultBreak;
		}
		else
		{
			IUseInteraction u;
			IBreakInteraction b;

			foreach (MonoBehaviour interaction in interactions)
			{
				if (interaction is IUseInteraction)
				{
					u = (IUseInteraction) interaction;
					useDelegate += u.Use;
				}
				if (interaction is IBreakInteraction)
				{
					b = (IBreakInteraction) interaction;
					breakDelegate += b.Break;
				}
			}
		}
	}

	protected void DefaultUse(int timesCalled)
	{
		Debug.Log ("Use " + name);
	}

	protected void DefaultBreak(int timesCalled)
	{
		Debug.Log ("Break " + name);
	}

	public void Use()
	{
		if (useDelegate != null)
		{
			useDelegate(++timesCalledUse);
		}
	}

	public void Break()
	{
		if (breakDelegate != null)
		{
			breakDelegate(++timesCalledBreak);
		}
	}
}

public interface IUseInteraction
{
	void Use(int timesCalled);
}

public interface IBreakInteraction
{
	void Break(int timesCalled);
}