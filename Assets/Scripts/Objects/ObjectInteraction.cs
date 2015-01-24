using UnityEngine;
using System.Collections;

public class ObjectInteraction : MonoBehaviour
{
	public MonoBehaviour[] interactions;

	protected delegate void UseDelegate ();
	protected UseDelegate useDelegate;

	protected delegate void BreakDelegate ();
	protected BreakDelegate breakDelegate;

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

	protected void DefaultUse()
	{
		Debug.Log ("Use " + name);
	}

	protected void DefaultBreak()
	{
		Debug.Log ("Break " + name);
	}

	public void Use()
	{
		if (useDelegate != null)
		{
			useDelegate();
		}
	}

	public void Break()
	{
		if (breakDelegate != null)
		{
			breakDelegate();
		}
	}
}

public interface IUseInteraction
{
	void Use();
}

public interface IBreakInteraction
{
	void Break();
}