using UnityEngine;
using System.Collections;

public class ShaderController : MonoBehaviour {

	public Material shaderMaterial;

	public Color NightBeginColor;
	public Color NightMiddleColor;
	public Color NightEndColor;
	public Color DayBeginColor;
	public Color DayMiddleColor;
	public Color DayEndColor;

	void Update() {

		setShaderAmbientColor();

	}

	private void setShaderAmbientColor ()
	{
		float dayRatio = DayNightController.getDayRatio();
		Color begin;
		Color middle;
		Color end;
		if (DayNightController.isDay()) {
			begin = DayBeginColor;
			middle = DayMiddleColor;
			end = DayEndColor;
		}
		else {
			begin = NightBeginColor;
			middle = NightMiddleColor;
			end = NightEndColor;
		}
		Color c;
		if (dayRatio < 0.5f) {
			c = new Color (MathHelper.Map (dayRatio, 0, 0.5f, begin.r, middle.r), 
			               MathHelper.Map (dayRatio, 0, 0.5f, begin.g, middle.g), 
			               MathHelper.Map (dayRatio, 0, 0.5f, begin.b, middle.b), 1);
		}
		else {
			c = new Color (MathHelper.Map (dayRatio, 0.5f, 1, middle.r, end.r), 
			               MathHelper.Map (dayRatio, 0.5f, 1, middle.g, end.g), 
			               MathHelper.Map (dayRatio, 0.5f, 1, middle.b, end.b), 1);
		}
		shaderMaterial.SetColor("_AmbientColor", c);

	}

}
