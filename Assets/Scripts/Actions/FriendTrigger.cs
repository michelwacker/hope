using UnityEngine;
using System.Collections;

public class FriendTrigger: MonoBehaviour, ITriggerAction
{
	public GameObject friend;
	
	#region ITriggerAction implementation
	public void Action ()
	{
		if (friend != null)
		{
			Friend f= friend.GetComponent<Friend>();
			if (f != null)
			{
				f.Action();
			}
		}
	}
	#endregion
}
