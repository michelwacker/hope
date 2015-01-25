using UnityEngine;
using System.Collections;

public class SoundTrigger : MonoBehaviour, ITriggerAction
{
	public AudioClip clip;
	private AudioSource source;

	void Awake()
	{
		source = GetComponent<AudioSource> ();
	}
	#region ITriggerAction implementation

	public void Action ()
	{
		if (source != null && clip != null)
		{
//			source.clip = clip;
			source.PlayOneShot(clip);
		}
	}

	#endregion


}
