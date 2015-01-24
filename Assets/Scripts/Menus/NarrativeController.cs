using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NarrativeController : MonoBehaviour
{
	public static void Write(string text)
	{
		instance.StartTextOutput (text);
	}
	private static NarrativeController instance;

	public Text output;
	// Use this for initialization
	void Awake ()
	{
		if (instance == null)
		{
			instance = this;
		}
	}

	void StartTextOutput(string text)
	{
		// TODO start coroutine here instead
		output.text = text;
	}
}
