using UnityEngine;
using UnityEditor;
using System.Collections;

public class SceneMenu: Editor
{
	[MenuItem("Open Scene/Scene1")]
	public static void OpenS1()
	{
		OpenScene ("Scene1");
	}
	private static void OpenScene(string name)
	{
		if (EditorApplication.SaveCurrentSceneIfUserWantsTo())
		{
			EditorApplication.OpenScene("Assets/Scenes/" + name + ".unity");
		}
	}
}
