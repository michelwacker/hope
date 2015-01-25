using UnityEngine;
using System.Collections;

public class ShaderControllerMenu : MonoBehaviour {

	public Material shaderMaterial;
	private Color currentAmbientColor = new Color();
	private Color currentLightColor = new Color();

	void Update() {
		shaderMaterial.SetVector("_InsanityVector", new Vector4(Random.Range(-0.01f, 0.01f), Random.Range(-0.02f, 0.02f), 0,0 ));
		shaderMaterial.SetVector("_InsanityVector2", new Vector4(Random.Range(-0.005f, 0.005f), Random.Range(-0.01f, 0.01f), 0,0 ));
	}
}
