using UnityEngine;
using System.Collections;

public class VolumetricLightCamera : MonoBehaviour {

	public Material baseMaterial;
	public Material endShader;
	
	void OnRenderImage (RenderTexture source, RenderTexture destination){

		Graphics.Blit(source,destination,baseMaterial);
		endShader.SetTexture("_VolumetricLight", destination);
	}
}
