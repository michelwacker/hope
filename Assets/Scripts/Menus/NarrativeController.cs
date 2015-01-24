using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Text;

public class NarrativeController : MonoBehaviour
{
	const float READ_STEP = 0.04f;

	public static void Write(string text)
	{
		instance.StartTextOutput (text);
	}
	private static NarrativeController instance;

	public Text output;
	private string textToBeWritten;
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
		StopCoroutine("writeSlowly");
		textToBeWritten = text;
		StartCoroutine("writeSlowly");
	}

	IEnumerator writeSlowly() 
	{
		StringBuilder currentText = new StringBuilder();
		foreach(char c in textToBeWritten) 
		{
			currentText.Append(c);
			output.text = currentText.ToString();
			yield return new WaitForSeconds (READ_STEP);
		}
	}
}
