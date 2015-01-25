using UnityEngine;
using System.Collections;

public class ShowObject : MonoBehaviour, ITriggerAction
{
	public enum ShowType
	{
		Hide,
		Show
	}
	public ShowType action = ShowType.Show;
	public GameObject[] targets;

	void Awake()
	{
		if (targets.Length == 0)
		{
			Debug.LogWarning("No targets set in ShowObject action of " + name);
		}
	}

	#region ITriggerAction implementation
	public void Action ()
	{
		if (targets.Length > 0)
		{
			foreach(GameObject t in targets)
			{
				t.SetActive(action == ShowType.Show);
			}
		}
	}
	#endregion
}
