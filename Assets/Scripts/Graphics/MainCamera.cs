﻿using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour {
	
	public Material material;
	public RenderTexture intermediateRT;

	void OnRenderImage (RenderTexture source, RenderTexture destination){	
		Graphics.Blit(source, intermediateRT, material);
		Graphics.Blit(intermediateRT, destination);
	}
}
